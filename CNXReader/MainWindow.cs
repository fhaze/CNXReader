using System;
using Gtk;
using CNXReader;

public partial class MainWindow : Gtk.Window
{
	ListStore       beneficiarioListStore;
	TreeModelFilter treeModelFilter;
	TreeModelSort   treeModelSort;

	int countFiltered;

	public MainWindow() : base(Gtk.WindowType.Toplevel)
	{
		Build();
		InitTreeView();

		treeView.Selection.Changed += Selection_Changed;
		filterBtnApply.Clicked += FilterBtnApply_Clicked;
		filterBtnClear.Clicked += FilterBtnClear_Clicked;
	}

	ListStore CreateBeneficiarioListStore()
	{
		return new ListStore(
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string));
	}

	void InitFilter()
	{
		if (cbSortable.Active)
		{
			treeModelFilter = new TreeModelFilter(beneficiarioListStore, null);
			treeModelFilter.VisibleFunc = new TreeModelFilterVisibleFunc(FilterTree);
			treeModelSort = new TreeModelSort(treeModelFilter);
			treeView.Model = treeModelSort;
		}
		else
		{
			treeModelFilter = new TreeModelFilter(beneficiarioListStore, null);
			treeModelFilter.VisibleFunc = new TreeModelFilterVisibleFunc(FilterTree);
			treeView.Model = treeModelFilter;
		}
	}

	void RemoveFilter()
	{
		beneficiarioListStore = CreateBeneficiarioListStore();
		treeView.Model = beneficiarioListStore;
	}

	void InitTreeView()
	{
		beneficiarioListStore = CreateBeneficiarioListStore();

		// CCO
		var ccoColumn = new SortableTreeViewColumn(0);
		ccoColumn.Title = "CCO";
		var ccoNameCell = new CellRendererText();
		ccoColumn.PackStart(ccoNameCell, true);
		ccoColumn.SetCellDataFunc(ccoNameCell, new TreeCellDataFunc(RenderCell));

		// Situacao
		var situacaoColumn = new SortableTreeViewColumn(1);
		situacaoColumn.Title = "Situação";
		var situacaoNameCell = new CellRendererText();
		situacaoColumn.PackStart(situacaoNameCell, true);
		situacaoColumn.SetCellDataFunc(situacaoNameCell, new TreeCellDataFunc(RenderCell));

		// Relação Dependência
		var relacaoDepColumn = new SortableTreeViewColumn(2);
		relacaoDepColumn.Title = "R";
		var relacaoDepNameCell = new CellRendererText();
		relacaoDepColumn.PackStart(relacaoDepNameCell, true);
		relacaoDepColumn.SetCellDataFunc(relacaoDepNameCell, new TreeCellDataFunc(RenderCell));

		// Código
		var codigoColumn = new SortableTreeViewColumn(3);
		codigoColumn.Title = "Código";
		var codigoNameCell = new CellRendererText();
		codigoColumn.PackStart(codigoNameCell, true);
		codigoColumn.SetCellDataFunc(codigoNameCell, new TreeCellDataFunc(RenderCell));

		// Nome
		var nomeColumn = new SortableTreeViewColumn(4);
		nomeColumn.Title = "Nome";
		var nomeNameCell = new CellRendererText();
		nomeColumn.PackStart(nomeNameCell, true);
		nomeColumn.SetCellDataFunc(nomeNameCell, new TreeCellDataFunc(RenderCell));

		// Sexo
		var sexoColumn = new SortableTreeViewColumn(5);
		sexoColumn.Title = "Sexo";
		var sexoNameCell = new CellRendererText();
		sexoColumn.PackStart(sexoNameCell, true);
		sexoColumn.SetCellDataFunc(sexoNameCell, new TreeCellDataFunc(RenderCell));

		// Mãe
		var maeColumn = new SortableTreeViewColumn(6);
		maeColumn.Title = "Mãe";
		var maeNameCell = new CellRendererText();
		maeColumn.PackStart(maeNameCell, true);
		maeColumn.SetCellDataFunc(maeNameCell, new TreeCellDataFunc(RenderCell));

		// CPF
		var cpfColumn = new SortableTreeViewColumn(7);
		cpfColumn.Title = "CPF";
		var cpfNameCell = new CellRendererText();
		cpfColumn.PackStart(cpfNameCell, true);
		cpfColumn.SetCellDataFunc(cpfNameCell, new TreeCellDataFunc(RenderCell));

		// CNS
		var cnsColumn = new SortableTreeViewColumn(8);
		cnsColumn.Title = "CNS";
		var cnsNameCell = new CellRendererText();
		cnsColumn.PackStart(cnsNameCell, true);
		cnsColumn.SetCellDataFunc(cnsNameCell, new TreeCellDataFunc(RenderCell));

		// Data de Nascimento
		var dtNascimentoColumn = new SortableTreeViewColumn(9);
		dtNascimentoColumn.Title = "Data Nascimento";
		var dtNascimentoCell = new CellRendererText();
		dtNascimentoColumn.PackStart(dtNascimentoCell, true);
		dtNascimentoColumn.SetCellDataFunc(dtNascimentoCell, new TreeCellDataFunc(RenderCell));

		// Plano ANS
		var planoAnsColumn = new SortableTreeViewColumn(10);
		planoAnsColumn.Title = "Plano ANS";
		var planoAnsNameCell = new CellRendererText();
		planoAnsColumn.PackStart(planoAnsNameCell, true);
		planoAnsColumn.SetCellDataFunc(planoAnsNameCell, new TreeCellDataFunc(RenderCell));

		// CNPJ Empresa Contratante
		var cnpjColumn = new SortableTreeViewColumn(11);
		cnpjColumn.Title = "CNPJ Empresa";
		var cnpjNameCell = new CellRendererText();
		cnpjColumn.PackStart(cnpjNameCell, true);
		cnpjColumn.SetCellDataFunc(cnpjNameCell, new TreeCellDataFunc(RenderCell));

		// Data Contratação
		var dtContratacaoColumn = new SortableTreeViewColumn(12);
		dtContratacaoColumn.Title = "Contratação";
		var dtContratacaoNameCell = new CellRendererText();
		dtContratacaoColumn.PackStart(dtContratacaoNameCell, true);
		dtContratacaoColumn.SetCellDataFunc(dtContratacaoNameCell, new TreeCellDataFunc(RenderCell));

		// Data Cancelamento
		var dtCancelamentoColumn = new SortableTreeViewColumn(13);
		dtCancelamentoColumn.Title = "Cancelamento";
		var dtCancelamentoNameCell = new CellRendererText();
		dtCancelamentoColumn.PackStart(dtCancelamentoNameCell, true);
		dtCancelamentoColumn.SetCellDataFunc(dtCancelamentoNameCell, new TreeCellDataFunc(RenderCell));

		// Data Reativação
		var dtReativacaoColumn = new SortableTreeViewColumn(14);
		dtReativacaoColumn.Title = "Reativação";
		var dtReativacaoNameCell = new CellRendererText();
		dtReativacaoColumn.PackStart(dtReativacaoNameCell, true);
		dtReativacaoColumn.SetCellDataFunc(dtReativacaoNameCell, new TreeCellDataFunc(RenderCell));

		// Data Atualização
		var dtAtualizacaoColumn = new SortableTreeViewColumn(15);
		dtAtualizacaoColumn.Title = "Última Atualização";
		var dtAtualizacaoNameCell = new CellRendererText();
		dtAtualizacaoColumn.PackStart(dtAtualizacaoNameCell, true);
		dtAtualizacaoColumn.SetCellDataFunc(dtAtualizacaoNameCell, new TreeCellDataFunc(RenderCell));

		// Ibge
		var ibgeColumn = new SortableTreeViewColumn(16);
		ibgeColumn.Title = "IBGE";
		var ibgeNameCell = new CellRendererText();
		ibgeColumn.PackStart(ibgeNameCell, true);
		ibgeColumn.SetCellDataFunc(ibgeNameCell, new TreeCellDataFunc(RenderCell));

		// Bairro
		var bairroColumn = new SortableTreeViewColumn(17);
		bairroColumn.Title = "Bairro";
		var bairroNameCell = new CellRendererText();
		bairroColumn.PackStart(bairroNameCell, true);
		bairroColumn.SetCellDataFunc(bairroNameCell, new TreeCellDataFunc(RenderCell));

		treeView.AppendColumn(ccoColumn);
		treeView.AppendColumn(situacaoColumn);
		treeView.AppendColumn(relacaoDepColumn);
		treeView.AppendColumn(codigoColumn);
		treeView.AppendColumn(nomeColumn);
		treeView.AppendColumn(sexoColumn);
		treeView.AppendColumn(maeColumn);
		treeView.AppendColumn(cpfColumn);
		treeView.AppendColumn(cnsColumn);
		treeView.AppendColumn(dtNascimentoColumn);
		treeView.AppendColumn(planoAnsColumn);
		treeView.AppendColumn(cnpjColumn);
		treeView.AppendColumn(dtContratacaoColumn);
		treeView.AppendColumn(dtCancelamentoColumn);
		treeView.AppendColumn(dtReativacaoColumn);
		treeView.AppendColumn(dtAtualizacaoColumn);
		treeView.AppendColumn(ibgeColumn);
		treeView.AppendColumn(bairroColumn);

		ccoColumn.AddAttribute(ccoNameCell, "text", 0);
		situacaoColumn.AddAttribute(situacaoNameCell, "text", 1);
		relacaoDepColumn.AddAttribute(relacaoDepNameCell, "text", 2);
		codigoColumn.AddAttribute(codigoNameCell, "text", 3);
		nomeColumn.AddAttribute(nomeNameCell, "text", 4);
		sexoColumn.AddAttribute(sexoNameCell, "text", 5);
		maeColumn.AddAttribute(maeNameCell, "text", 6);
		cpfColumn.AddAttribute(cpfNameCell, "text", 7);
		cnsColumn.AddAttribute(cnsNameCell, "text", 8);
		dtNascimentoColumn.AddAttribute(dtNascimentoCell, "text", 9);
		planoAnsColumn.AddAttribute(planoAnsNameCell, "text", 10);
		cnpjColumn.AddAttribute(cnpjNameCell, "text", 11);
		dtContratacaoColumn.AddAttribute(dtContratacaoNameCell, "text", 12);
		dtCancelamentoColumn.AddAttribute(dtCancelamentoNameCell, "text", 13);
		dtReativacaoColumn.AddAttribute(dtReativacaoNameCell, "text", 14);
		dtAtualizacaoColumn.AddAttribute(dtAtualizacaoNameCell, "text", 15);
		ibgeColumn.AddAttribute(ibgeNameCell, "text", 16);
		bairroColumn.AddAttribute(bairroNameCell, "text", 17);

		treeView.Model = beneficiarioListStore;
	}

	void RenderCell(TreeViewColumn column, CellRenderer cell, TreeModel model, TreeIter iter)
	{
		string situacao = (string)model.GetValue(iter, 1);

		if (situacao.Equals("INATIVO"))
			(cell as CellRendererText).Foreground = "red";
		else
			(cell as CellRendererText).Foreground = "black";
	}

	void Filter()
	{
		countFiltered = 0;
		InitFilter();
		statusBar.Push(0, $"{countFiltered} registro(s) filtrado(s)");
	}

	void FilterBtnApply_Clicked(object sender, EventArgs e)
	{
		Filter();
	}

	void FilterBtnClear_Clicked(object sender, EventArgs e)
	{
		filterCbApenasAtivos.Active = false;
		filterEntryCCO.Text = "";
		filterEntryCodigo.Text = "";
		filterEntryNome.Text = "";
		filterEntryCPF.Text = "";
		filterEntryCNS.Text = "";
		filterEntryDtNascimento.Text = "";
		filterEntryPlanoAns.Text = "";
	}

	bool FilterTree(TreeModel model, TreeIter iter)
	{
		if (model.GetValue(iter, 0) == null)
			return false;

		var cco =  model.GetValue(iter, 0).ToString();
		var situacao = model.GetValue(iter, 1).ToString();
		var codigo = model.GetValue(iter, 3).ToString();
		var nome = model.GetValue(iter, 4).ToString();
		var mae = model.GetValue(iter, 6).ToString();
		var cpf = model.GetValue(iter, 7).ToString();
		var cns = model.GetValue(iter, 8).ToString();
		var dtNascimento = model.GetValue(iter, 9).ToString();
		var planoAns = model.GetValue(iter, 10).ToString();
		var cnpj = model.GetValue(iter, 11).ToString();

		if (filterEntryCodigo.Text.Length > 0 && !codigo.Contains(filterEntryCodigo.Text))
			return false;

		if (filterEntryNome.Text.Length > 0 && !nome.ToUpper().Contains(filterEntryNome.Text.ToUpper()))
			return false;

		if (filterEntryMae.Text.Length > 0 && !mae.ToUpper().Contains(filterEntryMae.Text.ToUpper()))
			return false;

		if (filterEntryCPF.Text.Length > 0 && !cpf.Contains(filterEntryCPF.Text))
			return false;

		if (filterCbApenasAtivos.Active && situacao.Equals("INATIVO"))
			return false;

		if (filterEntryCCO.Text.Length > 0 && !cco.Contains(filterEntryCCO.Text))
			return false;

		if (filterEntryCNS.Text.Length > 0 && !cns.Contains(filterEntryCNS.Text))
			return false;

		if (filterEntryPlanoAns.Text.Length > 0 && !planoAns.Contains(filterEntryPlanoAns.Text))
			return false;

		if (filterEntryDtNascimento.Text.Length > 0 && !dtNascimento.Contains(filterEntryDtNascimento.Text))
			return false;

		if (filterEntryPlanoAns.Text.Length > 0 && !planoAns.Contains(filterEntryPlanoAns.Text))
			return false;

		if (filterEntryCNPJ.Text.Length > 0 && !cnpj.Contains(filterEntryCNPJ.Text))
			return false;

		countFiltered++;
		return true;
	}

	void Selection_Changed(object sender, EventArgs e)
	{
		TreeIter selected;
		if (treeView.Selection.GetSelected(out selected))
		{
			informativeDetails.Text =
				  $"CCO: {(string)treeView.Model.GetValue(selected, 0)}  Situação: {(string)treeView.Model.GetValue(selected, 1)}\n" +
				  $"Relação Dependência: {((string)treeView.Model.GetValue(selected, 2) == "T" ? "Titular" : "Dependente")  }\n" +
				  $"Código: {(string)treeView.Model.GetValue(selected, 3)}\n" +
				  $"Nome: {(string)treeView.Model.GetValue(selected, 4)}\n" +
				  $"Mãe: {(string)treeView.Model.GetValue(selected, 6)}\n" +
				  $"Última Atualização: {(string)treeView.Model.GetValue(selected, 15)}";
		}
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

	protected void OnAbrir(object sender, EventArgs e)
	{
		FileChooserDialog fc = new FileChooserDialog(
			"Escolha um arquivo CNX da ANS", this, FileChooserAction.Open,
			"Cancelar", ResponseType.Cancel,
			"Abrir", ResponseType.Accept);
		fc.Filter = new FileFilter();
		fc.Filter.AddPattern("*.cnx");
		fc.Filter.AddPattern("*.CNX");

		if (fc.Run() == (int)ResponseType.Accept)
		{
			RemoveFilter();

			var cnxReader = new CNXReader.CNXReader(fc.Filename);

			informativeHeader.Text =
				$"Operadora: {cnxReader.RegistroAns}\n" +
				$"Data Hora Transação: {cnxReader.DataHoraTransacao}\n\n" +
				$"Ativos: {cnxReader.Ativos}\n" +
				$"Inativos: {cnxReader.Inativos}\n" +
				$"Total: {cnxReader.Beneficiarios.Count}";

			foreach (var ben in cnxReader.Beneficiarios)
			{
				beneficiarioListStore.AppendValues(
					ben.CCO,
					ben.Situacao,
					ben.RelacaoDependencia,
					ben.Codigo,
					ben.Nome,
					ben.Sexo,
					ben.Mae,
					ben.CPF,
					ben.CNS,
					ben.DataNascimento,
					ben.NumeroPlanoAns,
					ben.CNPJEmpresaContratante,
					ben.DataContratacao,
					ben.DataCancelamento,
					ben.DataReativacao,
					ben.DataAtualizacao,
					ben.Ibge,
					ben.Bairro);
			}

			statusBar.Push(0, "Arquivo carregado com sucesso.");
		}

		fc.Destroy();
	}

	protected void OnSair(object sender, EventArgs e)
	{
		Application.Quit();
	}

	protected void OnSobre(object sender, EventArgs e)
	{
		var about = new AboutDialog();

		about.ProgramName = "CNXReader";
		about.Version = "1.21b";
		about.Comments = "Visualizador de arquivo de conferência da ANS (CNX).";
		about.Authors = new string[] { "FHaze (Eder Matumoto)" };

		about.Run();
		about.Destroy();
	}

	protected void OnExport(object sender, EventArgs e)
	{
		FileChooserDialog fc = new FileChooserDialog(
			"Exportar CSV", this, FileChooserAction.Save,
			"Cancelar", ResponseType.Cancel,
			"Salvar", ResponseType.Accept);

		if (fc.Run() == (int)ResponseType.Accept)
		{
			var cnxWriter = new CNXWriter(treeView, fc.Filename);
			cnxWriter.Write();
			statusBar.Push(0, $"Arquivo CSV exportado com sucesso em \"{fc.Filename}\".");
		}

		fc.Destroy();
	}

	protected void OnEntryActived(object sender, EventArgs e)
	{
		Filter();
	}
}
