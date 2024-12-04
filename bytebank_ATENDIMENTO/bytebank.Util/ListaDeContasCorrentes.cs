using bytebank.Modelos.Conta;

namespace bytebank_ATENDIMENTO.bytebank.Util;

public class ListaDeContasCorrentes
{
    public ContaCorrente[] _itens = null;
    public int _proximaPosicao = 0;
    public int Tamanho
    {
        get
        {
            return _proximaPosicao;
        }
    }

    // classe indexável, acessa ela como se fosse um índice
    public ContaCorrente this[int indice]
    {
        get
        {
            return RecuperarContaNoIndice(indice);
        }
    }

    public ListaDeContasCorrentes(int tamanhoInicial = 5)
    {
        _itens = new ContaCorrente[tamanhoInicial];
    }

    public void Adicionar(ContaCorrente item)
    {
        Console.WriteLine($"Adicionando item na posição {_proximaPosicao}");
        VerificarCapacidade(_proximaPosicao + 1);
        _itens[_proximaPosicao] = item;
        _proximaPosicao++;
    }

    private void VerificarCapacidade(int tamanhoNecessario)
    {
        if (_itens.Length >= tamanhoNecessario)
        {
            return;
        }

        Console.WriteLine("Aumentando a capacidade da lista!");
        ContaCorrente[] novoArray = new ContaCorrente[tamanhoNecessario];

        for (int i = 0; i < _itens.Length; i++)
        {
            novoArray[i] = _itens[i];
        }
        _itens = novoArray;
    }

    public ContaCorrente MaiorSaldo()
    {

        ContaCorrente conta = null;
        double maiorValor = 0;

        for (int i = 0; i < _itens.Length; i++)
        {
            if (_itens[i] != null)
            {
                if (maiorValor < _itens[i].Saldo)
                {
                    maiorValor = _itens[i].Saldo;
                    conta = _itens[i];
                }
            }

        }

        return conta;
    }

    public void Remover(ContaCorrente conta)
    {
        int indiceItem = -1;

        for (int i = 0; i < _proximaPosicao; i++)
        {
            ContaCorrente contaAtual = _itens[i];

            if (contaAtual == conta)
            {
                indiceItem = i;
                break;
            }
        }

        for (int i = indiceItem; i < _proximaPosicao - 1; i++)
        {
            _itens[i] = _itens[i + 1];
        }

        _proximaPosicao--;
        _itens[_proximaPosicao] = null;
    }

    public void ExibeLista()
    {
        for (int i = 0; i < _itens.Length; i++)
        {
            if (_itens[i] != null)
            {
                var conta = _itens[i];
                Console.WriteLine($" Indice[{i}] = " +
                    $"Conta:{conta.Conta} - " +
                    $"N° da Agência: {conta.Numero_agencia}");
            }
        }
    }

    public ContaCorrente RecuperarContaNoIndice(int indice)
    {
        if (indice < 0 || indice >= _proximaPosicao)
        {
            throw new ArgumentOutOfRangeException(nameof(indice));
        }

        return _itens[indice];
    }

}
