using Doctor.Forms;
using Doctor.Model;
using Doctor.UI.Forms;
using DoctorClient;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Doctor.Panels
{
    /// <summary>
    /// 窗体：即时通讯
    /// </summary>
    public partial class ContactsForm : Form
    {

        public ContactsForm()
        {
            InitializeComponent();
            LoginForm form = new LoginForm();
            form.TopLevel = false;
        }

        /// <summary>
        /// 双击事件：点击某个联系人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lv_contacts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView listView = sender as ListView;
            System.Windows.Forms.ListView.SelectedListViewItemCollection collection = listView.SelectedItems;
            string patientName = collection[0].Text;
            new InstantMessageForm(patientName).Show();
        }

        /// <summary>
        /// 加载窗体事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContactsForm_Load(object sender, EventArgs e)
        {
            //初始化通讯录列表显示
            InitListView();

            //即时通讯登录
            if (MyIMClient.Connect())
            {
                MyIMClient.Login(LoginStatus.UserInfo.Name);

                //离线推送信息查询
                MyIMClient.OfflineMsgChanged += (a) => { RefreshListView(a); };

                //联系人改变时
                MyIMClient.ContactsChanged += (a) => { RefreshContacts(a); };
            }
            else
            {
                MessageBox.Show("登录失败");
            }

            //添加修改分组的点击事件
            foreach (ToolStripMenuItem item in dropDown_groups.DropDownItems)
            {
                item.Click += item_Click;
            }
        }

        private void RefreshContacts(ContactsEventArgs e)
        {
            switch (e.Reason)
            {
                case ContactsEventArgs.EnumReason.Added:
                    SetNumMsgs(e.NewContact, 0, e.Group);
                    break;
                case ContactsEventArgs.EnumReason.Removed:
                    //在控件上删除指定的联系人
                    for (int i = 0; i < lv_contacts.Items.Count; i++)
                    {
                        if(e.RemovedContact.Equals(lv_contacts.Items[i].Text))
                        {
                            lv_contacts.Items.RemoveAt(i);
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        
        /// <summary>
        /// 初始化联系人列表
        /// </summary>
        private void InitListView()
        {
            //清除示例分组和联系人
            lv_contacts.Items.Clear();
            lv_contacts.Groups.Clear();

            //初始化分组信息（好友、陌生人等）
            foreach (var item in MyIMClient.Contacts.Keys)
            {
                lv_contacts.Groups.Add(item, item);
            }

            //添加联系人项到控件中
            foreach (var contacts in MyIMClient.Contacts)
            {
                foreach (var contact in contacts.Value)
	            {
                    SetNumMsgs(contact, 0, contacts.Key);
	            }
            }

            //将离线信息数量显示上去
            var groupedCount = from msg in MyIMClient.UnreadMsgs
                               group msg by msg.From into g
                               select new
                               {
                                   g.Key,
                                   NumMsgs = g.Count()
                               };

            foreach (var g in groupedCount)
	        {
                SetNumMsgs(g.Key, g.NumMsgs);
	        }
        }

        /// <summary>
        /// 在ListView上将指定用户的未读个数修改为指定数值，如果没有则添加
        /// </summary>
        /// <param name="username"></param>
        /// <param name="num"></param>
        private void SetNumMsgs(string username, int num, string groupName = null)
        {
            int i;
            bool found = false;
            for (i = 0; i < lv_contacts.Items.Count; i++)
            {
                if (username.Equals(lv_contacts.Items[i].Text))
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                lv_contacts.Items[i].SubItems[1].Text = num.ToString();
                if (num > 0)
                {
                    lv_contacts.Items[i].ForeColor = Color.Red;
                }
                else
                {
                    lv_contacts.Items[i].ForeColor = Color.Black;
                }
            }
            else
            {
                ListViewItem item = new ListViewItem();
                item.Group = lv_contacts.Groups[groupName];
                item.Text = username;
                item.SubItems.Add(num.ToString());
                if (num > 0)
                {
                    item.ForeColor = Color.Red;
                }
                else
                {
                    item.ForeColor = Color.Black;
                }
                lv_contacts.Items.Add(item);
            } 
        }

        private void RefreshListView(OfflineMsgEventArgs e)
        {
            if (this.lv_contacts.InvokeRequired)
            {
                MyIMClient.OfflineMsgChangedHandler handler = new MyIMClient.OfflineMsgChangedHandler(RefreshListView);
                this.Invoke(handler, e);
            }
            else
            {
                switch (e.Reason)
                {
                    case OfflineMsgEventArgs.EnumReason.MsgAdded:
                        //根据离线消息的寄信人分类统计各个信息
                        var groupedCount = from msg in MyIMClient.UnreadMsgs
                                           group msg by msg.From into g
                                           select new
                                           {
                                               g.Key,
                                               NumMsgs = g.Count(),
                                           };

                        foreach (var g in groupedCount)
                        {
                            SetNumMsgs(g.Key, g.NumMsgs, MyIMClient.UNFAMILIAR);
                        }
                        break;
                    case OfflineMsgEventArgs.EnumReason.MsgRead:
                        //把e.User对应的条目清零
                        SetNumMsgs(e.User, 0);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 键盘按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lv_contacts_KeyPress(object sender, KeyPressEventArgs e)
        {
            //按下-键
            if(e.KeyChar == '-') 
            {
                DeleteSelectedItem();
            }
        }

        private void DeleteSelectedItem()
        {
            if (lv_contacts.SelectedItems.Count == 0)
            {
                return;
            }

            ListViewItem selectedItem = lv_contacts.SelectedItems[0];
            string username = selectedItem.Text;
            if (MessageBox.Show("是否要删除联系人" + username, "删除", MessageBoxButtons.YesNo)
                == System.Windows.Forms.DialogResult.Yes)
            {
                MyIMClient.RemoveContact(username);
            }
        }
        
        /// <summary>
        /// 点击鼠标右键显示菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lv_contacts_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (lv_contacts.SelectedItems.Count == 1)
                {
                    ListViewItem selectedItem = lv_contacts.SelectedItems[0];
                    string username = selectedItem.Text;

                    foreach (ToolStripMenuItem item in dropDown_groups.DropDownItems)
                    {
                        item.Checked = item.Text.Equals(selectedItem.Group.Header);
                    }
                    itemContextMenuStrip.Show(lv_contacts, e.Location);
                }
                else
                {
                    lvContextMenuStrip.Show(lv_contacts, e.Location);
                }
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ChangeSelectedItemToGroup(item.Text);
        }

        /// <summary>
        /// 点击列表右键菜单的某一项时触发：选中一项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "menuItem_del":
                    DeleteSelectedItem();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 改变某联系人的分组到指定组
        /// </summary>
        /// <param name="groupName">指定组名称</param>
        private void ChangeSelectedItemToGroup(string groupName)
        {
            if (lv_contacts.SelectedItems.Count == 0)
            {
                return;
            }

            ListViewItem selectedItem = lv_contacts.SelectedItems[0];
            string username = selectedItem.Text;

            MyIMClient.ChangeGroup(username, groupName);
            selectedItem.Group = lv_contacts.Groups[groupName];
        }

        /// <summary>
        /// 点击列表右键菜单的某一项时触发：未选中任何一项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "menuItem_addContact":
                    AddContactForm form = new AddContactForm();
                    form.StartPosition = FormStartPosition.CenterParent;
                    form.ShowDialog();
                    break;
                default:
                    break;
            }
        }
    }
}
