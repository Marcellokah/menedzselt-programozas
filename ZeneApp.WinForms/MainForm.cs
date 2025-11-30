using System.ComponentModel;
using ZeneApp.Common;
using Microsoft.EntityFrameworkCore;

namespace ZeneApp.WinForms;

public class MainForm : Form
{
    private DataGridView dgvZenek;
    private MenuStrip menuStrip;
    private ZeneContext _context;

    public MainForm()
    {
        InitializeComponent();
        _context = new ZeneContext();
        RefreshData();
    }

    private void InitializeComponent()
    {
        this.Text = "Zenehallgatás - WinForms";
        this.Size = new Size(800, 600);

        // Menü létrehozása
        menuStrip = new MenuStrip();
        var menuFile = new ToolStripMenuItem("Menü");
        
        var itemAdd = new ToolStripMenuItem("Új zene hozzáadása");
        itemAdd.Click += (s, e) => OpenAddForm();
        
        var itemRefresh = new ToolStripMenuItem("Lista frissítése");
        itemRefresh.Click += (s, e) => RefreshData();

        menuFile.DropDownItems.Add(itemAdd);
        menuFile.DropDownItems.Add(itemRefresh);
        menuStrip.Items.Add(menuFile);

        // Grid létrehozása
        dgvZenek = new DataGridView();
        dgvZenek.Dock = DockStyle.Fill;
        dgvZenek.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvZenek.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvZenek.AllowUserToAddRows = false;
        dgvZenek.ReadOnly = true; // Alapból csak olvasás
        
        // Esemény a módosításhoz (dupla kattintás)
        dgvZenek.CellDoubleClick += DgvZenek_CellDoubleClick;

        this.Controls.Add(dgvZenek);
        this.Controls.Add(menuStrip);
        this.MainMenuStrip = menuStrip;
    }

    private void RefreshData()
    {
        // Adatok betöltése: Prioritás szerint csökkenő sorrend
        var data = _context.Zenek
            .OrderByDescending(z => z.Prioritas)
            .ToList();

        dgvZenek.DataSource = data;

        // Oszlopok beállítása (Prioritás elrejtése)
        if (dgvZenek.Columns["Prioritas"] != null) dgvZenek.Columns["Prioritas"].Visible = false;
        if (dgvZenek.Columns["Id"] != null) dgvZenek.Columns["Id"].Visible = false;
        if (dgvZenek.Columns["HosszFormazva"] != null) dgvZenek.Columns["HosszFormazva"].Visible = false;
    }

    private void OpenAddForm()
    {
        var form = new ZeneEditorForm();
        if (form.ShowDialog() == DialogResult.OK)
        {
            RefreshData();
        }
    }

    private void DgvZenek_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            var zene = (Zene)dgvZenek.Rows[e.RowIndex].DataBoundItem;
            // Csak a prioritás módosítható, de átadjuk az egész objektumot
            var form = new ZeneEditorForm(zene);
            if (form.ShowDialog() == DialogResult.OK)
            {
                RefreshData();
            }
        }
    }
}