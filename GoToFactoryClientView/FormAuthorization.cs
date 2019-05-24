using GoToWorkFactoryServiceDAL.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GoToWorkFactoryClientView
{
    public partial class FormAuthorization : Form
    {
        private readonly IClientService service;
        private int? id;

        public FormAuthorization( IClientService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            id = service.GetList().First(x => x.Email == textBoxEmail.Text).Id;
        }

        public int? getId()
        {
            return id;
        }
    }
}
