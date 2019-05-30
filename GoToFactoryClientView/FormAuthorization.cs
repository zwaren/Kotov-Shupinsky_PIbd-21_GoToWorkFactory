using GoToWorkFactoryServiceDAL.BindingModels;
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
            id = service.GetElement(new ClientBindingModel { Email = textBoxEmail.Text }).Id;
            MessageBox.Show("Вход прощол успешно", "Собщенье", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        public int? getId()
        {
            return id;
        }
    }
}
