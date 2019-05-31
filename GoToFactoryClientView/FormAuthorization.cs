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
        public int Id { get; private set; }

        public FormAuthorization(IClientService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                Id = service.GetElement(new ClientBindingModel { Email = textBoxEmail.Text }).Id;
                MessageBox.Show("Вход прошел успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                MessageBox.Show("Вход прошел не успешно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
