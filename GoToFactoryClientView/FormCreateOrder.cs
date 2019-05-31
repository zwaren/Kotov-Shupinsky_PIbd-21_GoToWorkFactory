using GoToWorkFactoryServiceDAL.BindingModels;
using GoToWorkFactoryServiceDAL.Interfaces;
using GoToWorkFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace GoToWorkFactoryClientView
{
    public partial class FormCreateOrder : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        

        private readonly IProductService serviceP;

        private readonly IClientMainService serviceM;

        public OrderBindingModel order;

        public FormCreateOrder(IProductService serviceP, IClientMainService serviceM)
        {
            InitializeComponent();
            this.serviceP = serviceP;
            this.serviceM = serviceM;
        }

        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            try
            {
                List<OrderProductViewModel> list = new List<OrderProductViewModel>();
                foreach (var op in order.OrderProducts)
                {
                    list.Add(new OrderProductViewModel
                    {
                        ProductName = serviceP.GetElement(op.ProductId).Name,
                        OrderId = op.OrderId,
                        ProductId = op.ProductId,
                        Count = op.Count
                    });
                }
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                textBoxSum.Text = order.Sum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                serviceM.CreateOrder(order);
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;                
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void checkBoxReserved_CheckedChanged(object sender, EventArgs e)
        {
            order.Reserved = checkBoxReserved.Checked;
        }
    }
}
