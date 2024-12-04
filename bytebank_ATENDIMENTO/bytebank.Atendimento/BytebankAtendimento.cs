using bytebank.Modelos.Conta;
using bytebank_ATENDIMENTO.bytebank.Exceptions;
using bytebank_ATENDIMENTO.bytebank.Util;
using System.Collections;

namespace bytebank_ATENDIMENTO.bytebank.Atendimento;

// desabilita os alertas de variéveis que podem ser nulas
#nullable disable
internal class BytebankAtendimento
{

    private List<ContaCorrente> _listaDeContas = new List<ContaCorrente>(){
    new ContaCorrente(95, "123456-X"){Saldo=100,Titular = new Cliente{Cpf="11111",Nome ="Henrique"}},
    new ContaCorrente(95, "951258-X"){Saldo=200,Titular = new Cliente{Cpf="22222",Nome ="Pedro"}},
    new ContaCorrente(94, "987321-W"){Saldo=60,Titular = new Cliente{Cpf="33333",Nome ="Marisa"}}
};


    public void AtendimentoCliente()
    {
        try
        {
            char opcao = '0';

            while (opcao != '6')
            {
                Console.Clear();
                Console.WriteLine("===============================");
                Console.WriteLine("===       Atendimento       ===");
                Console.WriteLine("===1 - Cadastrar Conta      ===");
                Console.WriteLine("===2 - Listar Contas        ===");
                Console.WriteLine("===3 - Remover Conta        ===");
                Console.WriteLine("===4 - Ordenar Contas       ===");
                Console.WriteLine("===5 - Pesquisar Conta      ===");
                Console.WriteLine("===6 - Sair do Sistema      ===");
                Console.WriteLine("===============================\n\n");

                Console.Write("Opção desejada: ");

                try
                {
                    opcao = Console.ReadLine()[0];
                }
                catch (Exception excecao)
                {
                    throw new ByteBankException(excecao.Message);
                }

                switch (opcao)
                {
                    case '1':
                        CadastrarConta();
                        break;
                    case '2':
                        ListarContas();
                        break;
                    case '3':
                        RemoverContas();
                        break;
                    case '4':
                        OrdenarContas();
                        break;
                    case '5':
                        PesquisarContas();
                        break;
                    case '6':
                        EncerrarAplicacao();
                        break;
                    default:
                        Console.WriteLine("Opcao não implementada.");
                        break;
                }
            }
        }
        catch (ByteBankException exception)
        {
            Console.WriteLine(exception.Message);
        }
    }

    private void EncerrarAplicacao()
    {
        Console.WriteLine("... Encerrando a aplicação ...");
        Console.ReadKey();
    }

    private void PesquisarContas()
    {
        Console.Clear();
        Console.WriteLine("===============================");
        Console.WriteLine("===    PESQUISAR CONTAS     ===");
        Console.WriteLine("===============================");
        Console.WriteLine("\n");
        Console.Write("Deseja pesquisar por (1) NUMERO DA CONTA ou (2)CPF TITULAR ou (3) Nº AGÊNCIA? : ");

        switch (int.Parse(Console.ReadLine()))
        {
            case 1:
                {
                    Console.Write("Informe o número da Conta: ");
                    string _numeroConta = Console.ReadLine();
                    ContaCorrente consultaConta = ConsultaPorNumeroConta(_numeroConta);
                    Console.WriteLine(consultaConta.ToString());
                    Console.ReadKey();
                    break;
                }
            case 2:
                {
                    Console.Write("Informe o CPF do Titular: ");
                    string _cpf = Console.ReadLine();
                    ContaCorrente consultaCpf = ConsultaPorCPFTitular(_cpf);
                    Console.WriteLine(consultaCpf.ToString());
                    Console.ReadKey();
                    break;
                }
            case 3:
                {
                    Console.Write("Informe o Nº da Agência: ");
                    int _numeroAgencia = int.Parse(Console.ReadLine());
                    var contasPorAgencia = ConsultaPorAgencia(_numeroAgencia);
                    ExibirListaDeContas(contasPorAgencia);
                    Console.ReadKey();
                    break;
                }
            default:
                Console.WriteLine("Opção não implementada.");
                break;
        }
    }

   private List<ContaCorrente> ConsultaPorAgencia(int numeroAgencia)
    {
        var consulta = (
                            from conta in _listaDeContas
                            where conta.Numero_agencia == numeroAgencia
                            select conta).ToList();
        return consulta;
    }

    private void ExibirListaDeContas(List<ContaCorrente> contasPorAgencia)
    {
        if (contasPorAgencia == null)
        {
            Console.WriteLine(" ... A consulta não retornou dados ...");
        }
        else
        {
            foreach (var item in contasPorAgencia)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }

    private ContaCorrente ConsultaPorCPFTitular(string? cpf)
    {
        return _listaDeContas
                  .Where(conta => conta.Titular.Cpf.Equals(cpf))
                  .FirstOrDefault();
    }

    private ContaCorrente ConsultaPorNumeroConta(string? numeroConta)
    {
        //ContaCorrente conta = null;
        //for (int i = 0; i < _listaDeContas.Count; i++)
        //{
        //    if (_listaDeContas[i].Conta.Equals(numeroConta))
        //    {
        //        conta = _listaDeContas[i];
        //    }
        //}

        return _listaDeContas
            .Where(e => e.Conta.Equals(numeroConta))
            .FirstOrDefault();
    }


    /*
      Para utilizar o método .Sort() da classe List<>, é necessário que a classe 
    daquele conjunto de objetos (ContaCorrente) tenham a interface IComparable implementada.
     */
    private void OrdenarContas()
    {
        _listaDeContas.Sort();
        Console.WriteLine("... Lista de Contas ordenadas ...");
        Console.ReadKey();
    }

    private void RemoverContas()
    {
        Console.Clear();
        Console.WriteLine("===============================");
        Console.WriteLine("===      REMOVER CONTAS     ===");
        Console.WriteLine("===============================");
        Console.WriteLine("\n");
        Console.Write("Informe o número da Conta: ");

        string numeroConta = Console.ReadLine();

        ContaCorrente conta = null;

        foreach (var item in _listaDeContas)
        {
            if (item.Conta.Equals(numeroConta))
            {
                conta = item;
            }
        }

        if (conta != null)
        {
            _listaDeContas.Remove(conta);
            Console.WriteLine("... Conta removida da lista! ...");
        }
        else
        {
            Console.WriteLine(" ... Conta para remoção não encontrada ...");
        }
        Console.ReadKey();
    }

    private void CadastrarConta()
    {
        Console.Clear();
        Console.WriteLine("===============================");
        Console.WriteLine("===   CADASTRO DE CONTAS    ===");
        Console.WriteLine("===============================");
        Console.WriteLine("\n");
        Console.WriteLine("=== Informe dados da conta ===");

        Console.Write("Número da Agência: ");
        int numeroAgencia = int.Parse(Console.ReadLine());

        ContaCorrente conta = new ContaCorrente(numeroAgencia);

        Console.WriteLine($"Número da conta [NOVA] : {conta.Conta}");

        Console.Write("Saldo inicial: ");
        conta.Saldo = double.Parse(Console.ReadLine());

        Console.Write("Nome do Titular: ");
        conta.Titular.Nome = Console.ReadLine();

        Console.Write("CPF do Titular: ");
        conta.Titular.Cpf = Console.ReadLine();

        Console.Write("Profissão do Titular: ");
        conta.Titular.Profissao = Console.ReadLine();

        _listaDeContas.Add(conta);
        Console.WriteLine("... Conta cadastrada com sucesso! ...");
        Console.ReadKey();
    }

    private void ListarContas()
    {
        Console.Clear();
        Console.WriteLine("===============================");
        Console.WriteLine("===     LISTA DE CONTAS     ===");
        Console.WriteLine("===============================\n");

        if (_listaDeContas.Count <= 0)
        {
            Console.WriteLine("... Não há contas cadastradas! ...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("===  Dados das Contas  ===");
        foreach (ContaCorrente item in _listaDeContas)
        {
            Console.WriteLine(item.ToString());
        }

    }

}
