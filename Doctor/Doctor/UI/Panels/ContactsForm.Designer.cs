namespace Doctor.Panels
{
    partial class ContactsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("好友", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("陌生人", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Tony",
            "123",
            "aa"}, -1, System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(185)))), ((int)(((byte)(153))))), System.Drawing.Color.Empty, null);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Sakura", System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(185)))), ((int)(((byte)(153))))), System.Drawing.SystemColors.Window, new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)))),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "10", System.Drawing.Color.Black, System.Drawing.SystemColors.Window, new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))))}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Pod",
            "0"}, -1);
            this.itemContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItem_del = new System.Windows.Forms.ToolStripMenuItem();
            this.dropDown_groups = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_friend = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_stranger = new System.Windows.Forms.ToolStripMenuItem();
            this.lvContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItem_addContact = new System.Windows.Forms.ToolStripMenuItem();
            this.lv_contacts = new System.Windows.Forms.ListView();
            this.username = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.unread_msg_num = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemContextMenuStrip.SuspendLayout();
            this.lvContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // itemContextMenuStrip
            // 
            this.itemContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_del,
            this.dropDown_groups});
            this.itemContextMenuStrip.Name = "contextMenuStrip";
            this.itemContextMenuStrip.Size = new System.Drawing.Size(133, 48);
            this.itemContextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_ItemClicked);
            // 
            // menuItem_del
            // 
            this.menuItem_del.Name = "menuItem_del";
            this.menuItem_del.Size = new System.Drawing.Size(132, 22);
            this.menuItem_del.Text = "删除(&D)";
            // 
            // dropDown_groups
            // 
            this.dropDown_groups.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_friend,
            this.menuItem_stranger});
            this.dropDown_groups.Name = "dropDown_groups";
            this.dropDown_groups.Size = new System.Drawing.Size(132, 22);
            this.dropDown_groups.Text = "移动至(&M)";
            // 
            // menuItem_friend
            // 
            this.menuItem_friend.Checked = true;
            this.menuItem_friend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItem_friend.Name = "menuItem_friend";
            this.menuItem_friend.Size = new System.Drawing.Size(127, 22);
            this.menuItem_friend.Text = "好友(&F)";
            // 
            // menuItem_stranger
            // 
            this.menuItem_stranger.Checked = true;
            this.menuItem_stranger.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItem_stranger.Name = "menuItem_stranger";
            this.menuItem_stranger.Size = new System.Drawing.Size(127, 22);
            this.menuItem_stranger.Text = "陌生人(&S)";
            // 
            // lvContextMenuStrip
            // 
            this.lvContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_addContact});
            this.lvContextMenuStrip.Name = "lvContextMenuStrip";
            this.lvContextMenuStrip.Size = new System.Drawing.Size(153, 26);
            this.lvContextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.lvContextMenuStrip_ItemClicked);
            // 
            // menuItem_addContact
            // 
            this.menuItem_addContact.Name = "menuItem_addContact";
            this.menuItem_addContact.Size = new System.Drawing.Size(152, 22);
            this.menuItem_addContact.Text = "添加联系人(&A)";
            // 
            // lv_contacts
            // 
            this.lv_contacts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(187)))), ((int)(((byte)(191)))));
            this.lv_contacts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lv_contacts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.username,
            this.unread_msg_num});
            this.lv_contacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_contacts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.lv_contacts.FullRowSelect = true;
            listViewGroup1.Header = "好友";
            listViewGroup1.Name = "friend";
            listViewGroup2.Header = "陌生人";
            listViewGroup2.Name = "stranger";
            this.lv_contacts.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.lv_contacts.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lv_contacts.HideSelection = false;
            listViewItem1.Group = listViewGroup1;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.Group = listViewGroup2;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.Group = listViewGroup2;
            listViewItem3.StateImageIndex = 0;
            this.lv_contacts.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.lv_contacts.LabelWrap = false;
            this.lv_contacts.Location = new System.Drawing.Point(0, 0);
            this.lv_contacts.MultiSelect = false;
            this.lv_contacts.Name = "lv_contacts";
            this.lv_contacts.Size = new System.Drawing.Size(803, 460);
            this.lv_contacts.TabIndex = 0;
            this.lv_contacts.UseCompatibleStateImageBehavior = false;
            this.lv_contacts.View = System.Windows.Forms.View.Details;
            this.lv_contacts.SelectedIndexChanged += new System.EventHandler(this.lv_contacts_SelectedIndexChanged);
            this.lv_contacts.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lv_contacts_KeyPress);
            this.lv_contacts.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lv_contacts_MouseDoubleClick);
            this.lv_contacts.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lv_contacts_MouseDown);
            this.lv_contacts.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lv_contacts_MouseUp);
            // 
            // username
            // 
            this.username.Text = "用户名";
            this.username.Width = 96;
            // 
            // unread_msg_num
            // 
            this.unread_msg_num.Text = "未读信息条数";
            this.unread_msg_num.Width = 288;
            // 
            // ContactsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 460);
            this.Controls.Add(this.lv_contacts);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ContactsForm";
            this.Text = "联系人";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ContactsForm_FormClosed);
            this.Load += new System.EventHandler(this.ContactsForm_Load);
            this.itemContextMenuStrip.ResumeLayout(false);
            this.lvContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader username;
        private System.Windows.Forms.ColumnHeader unread_msg_num;
        private System.Windows.Forms.ContextMenuStrip itemContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItem_del;
        private System.Windows.Forms.ToolStripMenuItem dropDown_groups;
        private System.Windows.Forms.ToolStripMenuItem menuItem_friend;
        private System.Windows.Forms.ToolStripMenuItem menuItem_stranger;
        private System.Windows.Forms.ContextMenuStrip lvContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItem_addContact;
        private System.Windows.Forms.ListView lv_contacts;


    }
}