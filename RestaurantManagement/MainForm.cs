using RestaurantManagement.Models;
using RestaurantManagement.Task;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace RestaurantManagement
{
    public partial class MainForm : Form
    {
        RestfullService restService = new RestfullService();
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            FillMenuNameBox();
        }
        private void menuList_SelectedIndexChanged(object sender, EventArgs e)
        {      
            if (menuList.SelectedIndex == 0) return;
            ComboboxItem selectedValue = (ComboboxItem)menuList.SelectedItem;
            FillContentofMenu((int)selectedValue.Value);
        }
        /// <summary>
        /// Fill Combobox with avaible menu that is getting from Restfull Service
        /// </summary>
        private void FillMenuNameBox()
        {
            menuList.Items.Insert(0, "Select Menu Name");
            menuList.SelectedIndex = 0;
            var allMenus =restService.ServiceResult<List<Models.Menu>>("Menu");
            foreach (var eachMenu in allMenus)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = eachMenu.MenuName;
                item.Value = eachMenu.MenuId;
                menuList.Items.Add(item);
            }
        }
        private void FillContentofMenu(int selectedMenuId)
        {
            if (selectedMenuId == 0) return;
            content.Items.Clear();
            var allItemsInMenu = restService.ServiceResult<List<Models.Menu>>("Menu", selectedMenuId);
            foreach (var eachItem in allItemsInMenu[0].Product)
            {
                content.Items.Add(eachItem.ProductName);
            }
        }
        private void content_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
