using Iss.LiveClassRoom.WebService.DataContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Iss.LiveClassRoom.TestUI.App_Code
{
    public partial class GetCourseList : Form
    {
        public GetCourseList()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var client = Client.getClient(emailTextBox.Text, pwdTextBox.Text);
            var courseList = client.GetCoursesByStudentEmail();
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("ID", typeof(string)));
            table.Columns.Add(new DataColumn("Course Name", typeof(string)));
            table.Columns.Add(new DataColumn("Instructor Name", typeof(string)));
            foreach (var item in courseList)
            {
                object[] objs = new object[3];
                objs[0] = item.Id;
                objs[1] = item.Name;
                objs[2] = item.Instructor;
                table.Rows.Add(objs);
            }
            DataView dataView = table.DefaultView;
            courseGridView.DataSource = dataView;
        }

        private void courseGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
