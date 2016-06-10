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
    public partial class GetMaterialListForm : Form
    {
        public GetMaterialListForm()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Invoke_Click(object sender, EventArgs e)
        {
            var client = Client.getClient(textBoxEmail.Text, textBoxPwd.Text);
            var videoList = client.GetVideoListByCourseName(textBoxCourseName.Text);
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("ID", typeof(string)));
            table.Columns.Add(new DataColumn("Title", typeof(string)));
            foreach (var item in videoList)
            {
                object[] objs = new object[2];
                objs[0] = item.Id;
                objs[1] = item.Title;
                table.Rows.Add(objs);
            }
            DataView dataView = table.DefaultView;
            dataGridViewMaterial.DataSource = dataView;
        }

        private void dataGridViewMaterial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
