using Newtonsoft.Json;
using PyWinForm_ConsumoAPI.ApiRest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PyWinForm_ConsumoAPI
{
    public partial class Form1 : Form
    {
        DBApi dbApi = new DBApi();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dynamic respuesta= dbApi.Get("https://dog.ceo/api/breeds/image/random");
            this.textBox1.Text = respuesta.message;
            this.textBox2.Text = respuesta.status;
            this.pictureBox1.ImageLocation = respuesta.message.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string json = "{\"name\": \"Franklin Cappa\",\"job\": \"Developer\"}";

            Persona persona = new Persona
            {
                name = this.textBox3.Text,
                job = this.textBox4.Text
            };

            string json = JsonConvert.SerializeObject(persona);
            dynamic respuesta = dbApi.Post("https://reqres.in/api/users", json);
            MessageBox.Show(respuesta.ToString());
        }

        /*
         {
    "message": "https://images.dog.ceo/breeds/malinois/n02105162_2836.jpg",
    "status": "success"
}
         */
        public class Persona
        {
            public string name { get; set; }
            public string job { get; set; }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dynamic respuesta = dbApi.Get("https://reqres.in/api/users?page=1");
            this.dataGridView1.DataSource = respuesta.data;


        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            foreach(DataGridView fila in dataGridView1.Rows)
            {
                //fila.Visible=fila.SelectedCells["firstName"].value.ToString().ToUpper().Contains(this.textBox5.Text.ToUpper());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dynamic respuesta = dbApi.GetItem(10);
            dbApi.GetItems();
            dbApi.GetItems("ABC");
            dbApi.PostItem("NewItem");
            dbApi.PutItem(4, "ReplaceItem");
            dbApi.DeleteItem(5);
        }
    }
}
