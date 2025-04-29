using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using text_modal.models;
using System.Globalization;
using text_modal.Extensions;

namespace text_modal
{
    public partial class modalForm : Form
    {
        private int member_id;
        private bool isDragging = false;
        private Point dragStartPoint;
        public modalForm(int id = 0)
        {
            InitializeComponent();
            this.Text = "";
            this.MouseDown += new MouseEventHandler(modalForm_MouseDown);
            this.MouseMove += new MouseEventHandler(modalForm_MouseMove);
            this.MouseUp += new MouseEventHandler(modalForm_MouseUp);
            if (id != 0)
            {
                this.member_id = id;
            }
           this.FormClosed += new FormClosedEventHandler(modalForm_FormClosed);

        }
        private void modalForm_Load(object sender, EventArgs e)
        {
            birthday.Format = DateTimePickerFormat.Custom;
            birthday.CustomFormat = "dd/MM/yyyy";

            if (this.Owner is Form1 form1)
            {
                

            if (member_id > 0) {
                form1.SetTitle("IT 01-3");
                IT_Entities _dbContext = new IT_Entities();
                btnSubmit.Visible= false;
                cancel.Text = "ปิด";
                var mem_model = _dbContext.mas_member.Where(x => x.member_id == member_id).FirstOrDefault();
                if (mem_model != null)
                {
                    firstname.Text = mem_model.firstname;
                    lastname.Text = mem_model.lastname;
                    birthday.Value = mem_model.birthday;
                    birthday.Enabled = false;
                    age.Text = birthday.Value.ToAge().ToString();
                    address.Text = mem_model.address;
                }
                else {
                    MessageBox.Show("เกิดข้อผิดพลาดในการเรียกใช้ข้อมูล!!");
                }
            }
            else
            {
                form1.SetTitle("IT 01-2");

                btnSubmit.Visible = true;
                age.Text = birthday.Value.ToAge().ToString();
                cancel.Text = "ยกเลิก";
            }
            }

            this.Location = new Point(Form1.parentX, Form1.parentY);
        }

        private void modalForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.Owner is Form1 form1)
            {
                form1.SetTitle(form1.formTitle); // Correct usage
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            member_id = 0;
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dateTime = birthday.Value;
                string test = firstname.Text;
                if (!String.IsNullOrEmpty(firstname.Text) && !String.IsNullOrEmpty(lastname.Text) &&  !String.IsNullOrEmpty(address.Text) && !string.IsNullOrEmpty(birthday.Value.ToString()))
                {
                    IT_Entities _dbContext = new IT_Entities();
                    mas_member memberModel = new mas_member();
                    memberModel.firstname = firstname.Text;
                    memberModel.lastname = lastname.Text;
                    memberModel.birthday = birthday.Value;
                    memberModel.address = address.Text;
                    memberModel.status = true;
                    _dbContext.mas_member.Add(memberModel);
                    _dbContext.SaveChanges();
                    MessageBox.Show("บันทึกข้อมูลสำเร็จ!!");
                    if (this.Owner is Form1 form1)
                    {
                        form1.ReloadMembertable();
                    }
                    this.Close();
                }
                else { 
                    MessageBox.Show("ตรวจสอบข้อมูลอีกครั้ง!!");
                }
            }
            catch (Exception ex) { 
                    MessageBox.Show("ตรวจสอบข้อมูลอีกครั้ง!!");
            }

        }

        private void birthday_ValueChanged(object sender, EventArgs e)
        {
            int calculatedAge = birthday.Value.ToAge();

            age.Text = calculatedAge.ToString();
        }

        // Event when the mouse is pressed down
        private void modalForm_MouseDown(object sender, MouseEventArgs e)
        {
            // When the mouse is pressed down, we start dragging
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Get the point where mouse is clicked
            }
        }

        // Event when the mouse is moving
        private void modalForm_MouseMove(object sender, MouseEventArgs e)
        {
            // If dragging is true, move the form
            if (isDragging)
            {
                // Calculate the new location of the form based on the mouse movement
                this.Location = new Point(this.Location.X + (e.X - dragStartPoint.X),
                                          this.Location.Y + (e.Y - dragStartPoint.Y));
            }
        }

        // Event when the mouse is released
        private void modalForm_MouseUp(object sender, MouseEventArgs e)
        {
            // Stop dragging when mouse is released
            isDragging = false;
        }

    }
}
