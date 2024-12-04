using bytebank.Modelos.Conta;
using bytebank_ATENDIMENTO.bytebank.Atendimento;
using bytebank_ATENDIMENTO.bytebank.Util;


Console.WriteLine("Boas Vindas ao ByteBank, Atendimento.");

#region Arrays em c#
TestaArrayInt();

void TestaArrayInt()
{
    int[] idades = new int[5];
    idades[0] = 30;
    idades[1] = 40;
    idades[2] = 17;
    idades[3] = 21;
    idades[4] = 18;

    // outras formas de se iniciar um array
    string[] palavras = new string[5] { "André", "Jose", "Andressa", "Neia", "Sarah" };
    double[] valores = { 2.6, 9.7, 7.5, 1.8 };

    Console.WriteLine($"Tamanho do Array {idades.Length}");

    int acumulador = 0;
    for (int i = 0; i < idades.Length; i++)
    {
        int idade = idades[i];
        Console.WriteLine($"índice [{i}] = {idade}");
        acumulador += idade;
    }

    int media = acumulador / idades.Length;
    Console.WriteLine($"Média de idades = {media}");

}

void TestaBuscarPalavra()
{
    string[] arrayDePalavras = new string[5];

    for (int i = 0; i < arrayDePalavras.Length; i++)
    {
        Console.Write($"Digite {i + 1}ª Palavra: ");
        arrayDePalavras[i] = Console.ReadLine();
    }


    Console.Write("Digite palavra a ser encontrada: ");
    var busca = Console.ReadLine();

    foreach (string palavra in arrayDePalavras)
    {
        if (palavra.Equals(busca))
        {
            Console.WriteLine($"Palavra encontrada = {busca}.");
            break;
        }
    }
}

//pode ser declarado assim também => Array amostra = Array.CreateInstance(typeof(double), 5);
Array amostra = new Double[5];
amostra.SetValue(5.9, 0);
amostra.SetValue(1.8, 1);
amostra.SetValue(7.1, 2);
amostra.SetValue(10, 3);
amostra.SetValue(6.9, 4);

TestaMediana(amostra);

void TestaMediana(Array array)
{
    if ((array == null) || (array.Length == 0))
    {
        Console.WriteLine("Array para cálculo da mediana está vazio ou nulo.");
    }

    double[] numerosOrdenados = (double[])array.Clone();
    Array.Sort(numerosOrdenados);

    int tamanho = numerosOrdenados.Length;
    int meio = tamanho / 2;

    double mediana = (tamanho % 2 != 0)
                    ? numerosOrdenados[meio]
                    : (numerosOrdenados[meio] + numerosOrdenados[meio - 1]) / 2;

    Console.WriteLine($"Com base na amostra a mediana = {mediana}");
}

double MediaDaAmostra(double[] amostra)
{
    double media = 0;
    double acumulador = 0;

    if ((amostra == null) || (amostra.Length == 0))
    {
        Console.WriteLine("Amostra de dados nula ou vazia.");
        return 0;
    }
    else
    {
        for (int i = 0; i < amostra.Length; i++)
        {
            acumulador = acumulador + amostra[i];
        }
        media = acumulador / amostra.Length;
    }

    return media;
}

TestaArrayDeContasCorrentes();
void TestaArrayDeContasCorrentes()
{

    ListaDeContasCorrentes listaDeContas = new ListaDeContasCorrentes();
    listaDeContas.Adicionar(new ContaCorrente(874));
    listaDeContas.Adicionar(new ContaCorrente(875));
    listaDeContas.Adicionar(new ContaCorrente(876));

    var contaDoAndre = new ContaCorrente(963);
    listaDeContas.Adicionar(contaDoAndre);
    listaDeContas.ExibeLista();

    Console.WriteLine("============");
    listaDeContas.Remover(contaDoAndre);
    listaDeContas.ExibeLista();

    for (int i = 0; i < listaDeContas.Tamanho; i++)
    {
        // ContaCorrente conta = listaDeContas.RecuperarContaNoIndice(i);
        ContaCorrente conta = listaDeContas[i];     // classe indexável, acessa ela como se fosse um índice


        Console.WriteLine($"Indice [{i}] = {conta.Conta}/{conta.Numero_agencia}");
    }

}

#endregion

#region métodos disponíveis no tipo list

List<ContaCorrente> _listaDeContas2 = new List<ContaCorrente>()
{
    new ContaCorrente(874, "5679787-A"),
    new ContaCorrente(874, "4456668-B"),
    new ContaCorrente(874, "7781438-C")
};

List<ContaCorrente> _listaDeContas3 = new List<ContaCorrente>()
{
    new ContaCorrente(951, "5679787-E"),
    new ContaCorrente(321, "4456668-F"),
    new ContaCorrente(719, "7781438-G")
};

_listaDeContas2.AddRange(_listaDeContas3); // adiciona uma lista na outra
_listaDeContas2.Reverse(); // inverte uma lista

for (int i = 0; i < _listaDeContas2.Count; i++)
{
    Console.WriteLine($"Indice[{i}] = Conta [{_listaDeContas2[i].Conta}]");
}

var range = _listaDeContas3.GetRange(0, 1); // cria uma nova lista a partir do range definido

for (int i = 0; i < range.Count; i++)
{
    Console.WriteLine($"Indice[{i}] = Conta [{range[i].Conta}]");
}

_listaDeContas3.Clear(); // remove todos os elementos da lista


List<string> nomesDosEscolhidos = new List<string>()
{
    "Bruce Wayne",
    "Carlos Vilagran",
    "Richard Grayson",
    "Bob Kane",
    "Will Farrel",
    "Lois Lane",
    "General Welling",
    "Perla Letícia",
    "Uxas",
    "Diana Prince",
    "Elisabeth Romanova",
    "Anakin Wayne"
};

Console.WriteLine($"O nome está na lista? " + VerificaNomes(nomesDosEscolhidos, "Anakin Wayne"));

bool VerificaNomes(List<string> nomesDosEscolhidos, string escolhido)
{
    return nomesDosEscolhidos.Contains(escolhido);
}

#endregion

new BytebankAtendimento().AtendimentoCliente();