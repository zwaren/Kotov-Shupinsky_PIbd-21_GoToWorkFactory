using System;
using System.Windows.Forms;
using GoToWorkFactoryServiceDAL.Interfaces;
using GoToWorkFactoryServiceDAL.ViewModels;
using GoToWorkFactoryServiceDAL.BindingModels;
using Unity;
using System.Collections.Generic;

namespace GoToWorkFactoryClientView
{
    public partial class FormMain : Form
    {
        int? clientId;
        string email;
        OrderBindingModel order;

        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IClientMainService service;
        private readonly IClientService cService;
        private readonly IProductService pService;
        private readonly IReportService rService;

        public FormMain(IClientMainService service, IClientService cService, IProductService pService, IReportService rService)
        {
            InitializeComponent();
            this.service = service;
            this.cService = cService;
            this.pService = pService;
            this.rService = rService;
            order = new OrderBindingModel();
            order.OrderProducts = new List<OrderProductBindingModel>();
        }

        private void LoadData()
        {
            try
            {
                List<ProductViewModel> list = pService.GetFilteredList();
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void зарегестрироватьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormClient>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void войтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormAuthorization>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var id = form.getId();
                var user = cService.GetElement(id.Value);
                if (user != null)
                {
                    clientId = id;
                    email = form.getEmail();
                }
                labelUser.Text = user.Name;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                var pId = Convert.ToInt32(row.Cells[0].Value);
                if (!order.OrderProducts.Exists(op => op.ProductId == pId))
                    order.OrderProducts.Add(new OrderProductBindingModel { ProductId = pId, Count = 0 });
                order.OrderProducts.Find(op => op.ProductId == pId).Count += 1;
            }
        }

        private void buttonCommit_Click(object sender, EventArgs e)
        {
            try
            {
                order.ClientId = clientId.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вы должны быть залогинены!\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
            order.Sum = 0;
            foreach (var op in order.OrderProducts)
                order.Sum += pService.GetElement(op.ProductId).Price;

            var form = Container.Resolve<FormCreateOrder>();
            form.order = order;
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }

            order = new OrderBindingModel();
            order.OrderProducts = new List<OrderProductBindingModel>();
            LoadData();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            rService.createMaterialRequest(new ReportBindingModel
            {
                Email = email,
                FileName = @"D:\test.docx",
                DateFrom = new DateTime(2018, 1, 1),
                DateTo = DateTime.Now
            });
        }
    }
}
