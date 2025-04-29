using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using text_modal.components;
using text_modal.Extensions;
using text_modal.models;

namespace text_modal
{
    public partial class Form1 : Form
    {
        Helpers _helpers = new Helpers();
        private string ColorBar { get; set; } = "#00b050";
        public string formTitle { get; set; } = "IT 01-1";
        public Form1()
        {
            InitializeComponent();
            ChangeTitleBarColor(ColorBar);
            this.Text = "";
            this.ShowIcon = false;
            this.Resize += new System.EventHandler(this.Form1_Resize);
        }

        private void ChangeTitleBarColor(string hexColor)
        {
            // Convert HEX string to Color
            Color color = ColorTranslator.FromHtml(hexColor);

            // Prepare color value
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_CAPTION_COLOR;
            uint colorValue = (uint)((color.R) | (color.G << 8) | (color.B << 16));

            DwmSetWindowAttribute(this.Handle, attribute, ref colorValue, sizeof(uint));
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, ref uint pvAttribute, int cbAttribute);

        private enum DWMWINDOWATTRIBUTE
        {
            DWMWA_CAPTION_COLOR = 35,
        }
        public static int parentX, parentY;

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.BackColor = _helpers.HaxToColor(ColorBar);
            label1.Location = new Point(0,0);
            label1.Size = new Size(this.Width,40);
            label1.Text = "IT 01-1";
            SetMemberTable();
            button1.Location = new Point(this.Width - button1.Width - 30,button1.Location.Y);
        }

        private void SetMemberTable() {

            member_table.BorderStyle = BorderStyle.None;
            member_table.RowHeadersVisible = false;
            member_table.Size = new Size(this.Width, member_table.Height);
            member_table.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            List<MemberView> memberViews = getMember();
            member_table.DataSource = memberViews;

            member_table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataGridViewButtonColumn buttons = new DataGridViewButtonColumn();
            {
                buttons.Name = "action";
                buttons.HeaderText = "Action";
                buttons.Text = "View";
                buttons.UseColumnTextForButtonValue = true;
                buttons.DefaultCellStyle.Padding = new Padding(20, 0, 20, 0);
                buttons.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                buttons.FlatStyle = FlatStyle.Standard;
            }
            member_table.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            member_table.ColumnHeadersDefaultCellStyle.BackColor = _helpers.HaxToColor("#00b0f0");

            member_table.Columns.Add(buttons);
            member_table.Width = this.Width - 40;
            member_table.Columns["member_id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            member_table.Columns["fullname"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            member_table.Columns["address"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            member_table.Columns["birthday"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            member_table.Columns["age"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        private void Member_table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == member_table.Columns["action"].Index)
                {
                    int memberId = Convert.ToInt32(member_table.Rows[e.RowIndex].Cells["member_id"].Value);
                    ShowModal(memberId);
                }
            }
            catch (Exception ex) { }
          
        }

        public void ReloadMembertable()
        {
            List<MemberView> memberViews = getMember();
            member_table.ClearSelection();
            member_table.DataSource = memberViews;
        }


        public List<MemberView> getMember() {
            IT_Entities _dbContext = new IT_Entities();
            List<MemberView> model = 
                          _dbContext.mas_member.AsEnumerable().Select(x => new MemberView
                          {
                              member_id = x.member_id,
                              fullname = x.firstname + " " + x.lastname,
                              address = x.address,
                              birthday = x.birthday.AddYears(543).ToString("dd/MM/yyyy"),
                              age = x.birthday.ToAge() // Use the extension method ToAge
                          })
                          .ToList();
            return model;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            int width = this.Width;
            int height = this.Height;
            button1.Location = new Point(this.Width - button1.Width - 30, button1.Location.Y);

            label1.Size = new Size(this.Width, 40);
        }

        public void SetTitle(string text)
        {
            label1.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowModal();
        }

        private void ShowModal(int mem_id = 0)
        {
            using (Form form = new Form())
            using (modalForm modal = new modalForm(mem_id))
            {
                form.StartPosition = FormStartPosition.Manual;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Opacity = 0.5d;
                form.BackColor = Color.Black;
                form.Size = this.Size;
                form.ShowInTaskbar = false;

                // Calculate center position
                int centerX = this.Location.X + (this.Width - form.Width) / 2;
                int centerY = this.Location.Y + (this.Height - form.Height) / 2;

                // Set the location of the form to center it
                form.Location = new Point(centerX, centerY);

                form.Show();

                modal.Owner = this;
                parentX = this.Location.X;
                parentY = this.Location.Y;

                modal.ShowDialog();
                form.Dispose();
            }
        }

    }
}
