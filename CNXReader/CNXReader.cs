using System;
using System.Collections.Generic;
using System.Xml;

namespace CNXReader
{
	public class CNXReader
	{
		public long Ativos { get; private set; }
		public long Inativos { get; private set; }
		public string RegistroAns { get; private set; }
		public string DataHoraTransacao { get; private set; }
		public List<Beneficiario> Beneficiarios { get; private set; }
		XmlDocument xml;

		public CNXReader(string cnx)
		{
			Beneficiarios = new List<Beneficiario>();
			xml = new XmlDocument();
			xml.Load(cnx);

			Ativos = 0;
			Inativos = 0;

			ProcessarCabecalho();
			ProcessarBeneficiarios();
		}

		void ProcessarCabecalho()
		{
			var node = xml.DocumentElement.SelectSingleNode("/mensagemSIB/cabecalho");

			RegistroAns = node.SelectSingleNode("destino").InnerText;
			DataHoraTransacao = node.SelectSingleNode("identificacaoTransacao/dataHoraRegistroTransacao").InnerText;
		}

		void ProcessarBeneficiarios()
		{
			var nodes = xml.DocumentElement.SelectNodes("/mensagemSIB/mensagem/ansParaOperadora/conferencia/beneficiario");

			foreach (XmlNode node in nodes)
			{
				var situacao = node.Attributes["situacao"].Value;

				if (situacao.Equals("ATIVO"))
					Ativos++;
				else
					Inativos++;

				Beneficiarios.Add(new Beneficiario
				{
					CCO = node.Attributes["cco"].Value,
					Codigo = node.SelectSingleNode("vinculo/codigoBeneficiario").InnerText,
					Situacao = situacao,
					Nome = node.SelectSingleNode("identificacao/nome").InnerText,
					Sexo = node.SelectSingleNode("identificacao/sexo").InnerText.Equals("1") ? "M" : "F",
					DataNascimento = node.SelectSingleNode("identificacao/dataNascimento") == null ? "" : node.SelectSingleNode("identificacao/dataNascimento").InnerText,
					CPF = node.SelectSingleNode("identificacao/cpf") == null ? "" : node.SelectSingleNode("identificacao/cpf").InnerText,
					CNS = node.SelectSingleNode("identificacao/cns") == null ? "" : node.SelectSingleNode("identificacao/cns").InnerText,
					Mae = node.SelectSingleNode("identificacao/nomeMae") == null ? "" : node.SelectSingleNode("identificacao/nomeMae").InnerText,
					RelacaoDependencia = node.SelectSingleNode("vinculo/relacaoDependencia") == null ? "" : node.SelectSingleNode("vinculo/relacaoDependencia").InnerText.Equals("1") ? "T" : "",
					DataContratacao = node.SelectSingleNode("vinculo/dataContratacao").InnerText,
					DataCancelamento = node.SelectSingleNode("vinculo/dataCancelamento") == null ? "" : node.SelectSingleNode("vinculo/dataCancelamento").InnerText,
					DataReativacao = node.SelectSingleNode("vinculo/dataReativacao") == null ? "" : node.SelectSingleNode("vinculo/dataReativacao").InnerText,
					NumeroPlanoAns = node.SelectSingleNode("vinculo/numeroPlanoANS") == null ? "" : node.SelectSingleNode("vinculo/numeroPlanoANS").InnerText,
					CNPJEmpresaContratante = node.SelectSingleNode("vinculo/cnpjEmpresaContratante") == null ? "" : node.SelectSingleNode("vinculo/cnpjEmpresaContratante").InnerText,
					DataAtualizacao = node.Attributes["dataAtualizacao"].InnerText,
					Ibge = node.SelectSingleNode("endereco/codigoMunicipio") == null ? "" : node.SelectSingleNode("endereco/codigoMunicipio").InnerText,
					Bairro = node.SelectSingleNode("endereco/bairro") == null ? "" : node.SelectSingleNode("endereco/bairro").InnerText
				});
			}
		}
	}
}
