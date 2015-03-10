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
            "aa"}, -1, System.Drawing.Color.Red, System.Drawing.Color.Empty, null);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Sakura", System.Drawing.Color.Red, System.Drawing.SystemColors.Window, new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)))),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "10", System.Drawing.Color.Black, System.Drawing.SystemColors.Window, new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))))}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Pod",
            "0"}, -1);
            this.lv_contacts = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItem_del = new System.Windows.Forms.ToolStripMenuItem();
            this.dropDown_groups = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_familiar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_unfamilar = new System.Windows.Forms.ToolStripMenuItem();
            this.lvContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItem_addContact = new System.Windows.Forms.ToolStripMenuItem();
            this.添加组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemContextMenuStrip.SuspendLayout();
            this.lvContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv_contacts
            // 
            this.lv_contacts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lv_contacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_contacts.FullRowSelect = true;
            listViewGroup1.Header = "好友";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "陌生人";
            listViewGroup2.Name = "listViewGroup2";
            this.lv_contacts.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            listViewItem1.Group = listViewGroup1;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.Group = listViewGroup2;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.Group = listViewGroup2;
            this.lv_contacts.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.lv_contacts.LabelWrap = false;
            this.lv_contacts.Location = new System.Drawing.Point(0, 0);
            this.lv_contacts.MultiSelect = false;
            this.lv_contacts.Name = "lv_contacts";
            this.lv_contacts.Size = new System.Drawing.Size(437, 297);
            this.lv_contacts.TabIndex = 0;
            this.lv_contacts.UseCompatibleStateImageBehavior = false;
            this.lv_contacts.View = System.Windows.Forms.View.Details;
            this.lv_contacts.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lv_contacts_KeyPress);
            this.lv_contacts.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lv_contacts_MouseDoubleClick);
            this.lv_contacts.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lv_contacts_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "用户名";
            this.columnHeader1.Width = 96;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "未读信息条数";
            this.columnHeader2.Width = 288;
            // 
            // itemContextMenuStrip
            // 
            this.itemContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_del,
            this.dropDown_groups});
            this.itemContextMenuStrip.Name = "contextMenuStrip";
            this.itemContextMenuStrip.Size = new System.Drawing.Size(113, 48);
            this.itemContextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_ItemClicked);
            // 
            // menuItem_del
            // 
            this.menuItem_del.Name = "menuItem_del";
            this.menuItem_del.Size = new System.Drawing.Size(112, 22);
            this.menuItem_del.Text = "删除";
            // 
            // dropDown_groups
            // 
            this.dropDown_groups.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_familiar,
            this.menuItem_unfamilar});
            this.dropDown_groups.Name = "dropDown_groups";
            this.dropDown_groups.Size = new System.Drawing.Size(112, 22);
            this.dropDown_groups.Text = "移动至";
            // 
            // menuItem_familiar
            // 
            this.menuItem_familiar.Checked = true;
            this.menuItem_familiar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItem_familiar.Name = "menuItem_familiar";
            this.menuItem_familiar.Size = new System.Drawing.Size(112, 22);
            this.menuItem_familiar.Text = "好友";
            // 
            // menuItem_unfamilar
            // 
            this.menuItem_unfamilar.Checked = true;
            this.menuItem_unfamilar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItem_unfamilar.Name = "menuItem_unfamilar";
            this.menuItem_unfamilar.Size = new System.Drawing.Size(112, 22);
            this.menuItem_unfamilar.Text = "陌生人";
            // 
            // lvContextMenuStrip
            // 
            this.lvContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_addContact,
            this.添加组ToolStripMenuItem});
            this.lvContextMenuStrip.Name = "lvContextMenuStrip";
            this.lvContextMenuStrip.Size = new System.Drawing.Size(137, 48);
            this.lvContextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.lvContextMenuStrip_ItemClicked);
            // 
            // menuItem_addContact
            // 
            this.menuItem_addContact.Name = "menuItem_addContact";
            this.menuItem_addContact.Size = new System.Drawing.Size(136, 22);
            this.menuItem_addContact.Text = "添加联系人";
            // 
            // 添加组ToolStripMenuItem
            // 
            this.添加组ToolStripMenuItem.Name = "添加组ToolStripMenuItem";
            this.添加组ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.添加组ToolStripMenuItem.Text = "添加组";
            // 
            // ContactsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 297);
            this.Controls.Add(this.lv_contacts);
            this.Name = "ContactsForm";
            this.Text = "联系人";
            this.Load += new System.EventHandler(this.ContactsForm_Load);
            this.itemContextMenuStrip.ResumeLayout(false);
            this.lvContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lv_contacts;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip itemContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItem_del;
        private System.Windows.Forms.ToolStripMenuItem dropDown_groups;
        private System.Windows.Forms.ToolStripMenuItem menuItem_familiar;
        private System.Windows.Forms.ToolStripMenuItem menuItem_unfamilar;
        private System.Windows.Forms.ContextMenuStrip lvContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItem_addContact;
        private System.Windows.Forms.ToolStripMenuItem 添加组ToolStripMenuItem;


    }
}