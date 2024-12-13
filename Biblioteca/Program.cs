using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// Model: Livro
public class Livro
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Genero { get; set; }
    public int AnoPublicacao { get; set; }

    public override string ToString() =>
        $"{Id} - {Titulo} ({AnoPublicacao}) - Autor: {Autor} - Gênero: {Genero}";
}

// Interface do Repositório de Livros
public interface ILivroRepository
{
    Task<List<Livro>> ObterTodosAsync();
    Task<Livro> ObterPorIdAsync(int id);
    Task AdicionarAsync(Livro livro);
    Task AtualizarAsync(Livro livro);
    Task<bool> ExcluirAsync(int id);
    Task<List<Livro>> BuscarPorAutorAsync(string autor);
    Task<List<Livro>> BuscarPorGeneroAsync(string genero);
}

// Repositório de livros (Simulação de um banco de dados)
public class LivroRepository : ILivroRepository
{
    private readonly List<Livro> _livros;
    private readonly ILogger<LivroRepository> _logger;

    public LivroRepository(ILogger<LivroRepository> logger)
    {
        _livros = new List<Livro>
        {
            new Livro { Id = 1, Titulo = "1984", Autor = "George Orwell", Genero = "Distopia", AnoPublicacao = 1949 },
            new Livro { Id = 2, Titulo = "O Hobbit", Autor = "J.R.R. Tolkien", Genero = "Fantasia", AnoPublicacao = 1937 },
            new Livro { Id = 3, Titulo = "Dom Quixote", Autor = "Miguel de Cervantes", Genero = "Clássico", AnoPublicacao = 1605 }
        };
        _logger = logger;
    }

    // Obter todos os livros
    public async Task<List<Livro>> ObterTodosAsync() =>
        await Task.FromResult(_livros);

    // Obter livro por ID
    public async Task<Livro> ObterPorIdAsync(int id) =>
        await Task.FromResult(_livros.FirstOrDefault(l => l.Id == id));

    // Adicionar um livro
    public async Task AdicionarAsync(Livro livro)
    {
        livro.Id = _livros.Max(l => l.Id) + 1;
        _livros.Add(livro);
        _logger.LogInformation($"Livro '{livro.Titulo}' adicionado com sucesso.");
        await Task.CompletedTask;
    }

    // Atualizar um livro
    public async Task AtualizarAsync(Livro livro)
    {
        var livroExistente = _livros.FirstOrDefault(l => l.Id == livro.Id);
        if (livroExistente == null)
        {
            _logger.LogWarning($"Livro com ID {livro.Id} não encontrado.");
            return;
        }

        livroExistente.Titulo = livro.Titulo;
        livroExistente.Autor = livro.Autor;
        livroExistente.Genero = livro.Genero;
        livroExistente.AnoPublicacao = livro.AnoPublicacao;
        _logger.LogInformation($"Livro '{livro.Titulo}' atualizado com sucesso.");
        await Task.CompletedTask;
    }

    // Excluir um livro
    public async Task<bool> ExcluirAsync(int id)
    {
        var livro = _livros.FirstOrDefault(l => l.Id == id);
        if (livro == null) return false;

        _livros.Remove(livro);
        _logger.LogInformation($"Livro '{livro.Titulo}' excluído com sucesso.");
        return await Task.FromResult(true);
    }

    // Buscar livros por autor
    public async Task<List<Livro>> BuscarPorAutorAsync(string autor)
    {
        var resultado = _livros.Where(l => l.Autor.Contains(autor, StringComparison.OrdinalIgnoreCase)).ToList();
        return await Task.FromResult(resultado);
    }

    // Buscar livros por gênero
    public async Task<List<Livro>> BuscarPorGeneroAsync(string genero)
    {
        var resultado = _livros.Where(l => l.Genero.Contains(genero, StringComparison.OrdinalIgnoreCase)).ToList();
        return await Task.FromResult(resultado);
    }
}

// Serviço de Biblioteca
public class BibliotecaService
{
    private readonly ILivroRepository _livroRepository;
    private readonly ILogger<BibliotecaService> _logger;

    public BibliotecaService(ILivroRepository livroRepository, ILogger<BibliotecaService> logger)
    {
        _livroRepository = livroRepository;
        _logger = logger;
    }

    public async Task ListarLivrosAsync()
    {
        var livros = await _livroRepository.ObterTodosAsync();
        if (livros.Any())
        {
            livros.ForEach(l => Console.WriteLine(l));
        }
        else
        {
            _logger.LogInformation("Nenhum livro encontrado.");
            Console.WriteLine("Nenhum livro encontrado.");
        }
    }

    public async Task AdicionarLivroAsync(string titulo, string autor, string genero, int anoPublicacao)
    {
        if (string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(autor) || anoPublicacao <= 0)
        {
            _logger.LogWarning("Tentativa de adicionar livro com dados inválidos.");
            Console.WriteLine("Dados inválidos. Tente novamente.");
            return;
        }

        var livro = new Livro
        {
            Titulo = titulo,
            Autor = autor,
            Genero = genero,
            AnoPublicacao = anoPublicacao
        };

        await _livroRepository.AdicionarAsync(livro);
        Console.WriteLine("Livro adicionado com sucesso.");
    }

    public async Task AtualizarLivroAsync(int id, string titulo, string autor, string genero, int anoPublicacao)
    {
        if (string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(autor) || anoPublicacao <= 0)
        {
            _logger.LogWarning("Tentativa de atualizar livro com dados inválidos.");
            Console.WriteLine("Dados inválidos. Tente novamente.");
            return;
        }

        var livro = new Livro
        {
            Id = id,
            Titulo = titulo,
            Autor = autor,
            Genero = genero,
            AnoPublicacao = anoPublicacao
        };

        await _livroRepository.AtualizarAsync(livro);
        Console.WriteLine("Livro atualizado com sucesso.");
    }

    public async Task ExcluirLivroAsync(int id)
    {
        var sucesso = await _livroRepository.ExcluirAsync(id);
        if (sucesso)
        {
            Console.WriteLine("Livro excluído com sucesso.");
        }
        else
        {
            Console.WriteLine("Livro não encontrado.");
        }
    }

    public async Task BuscarLivrosPorAutorAsync(string autor)
    {
        var livros = await _livroRepository.BuscarPorAutorAsync(autor);
        if (livros.Any())
        {
            livros.ForEach(l => Console.WriteLine(l));
        }
        else
        {
            Console.WriteLine("Nenhum livro encontrado para o autor.");
        }
    }

    public async Task BuscarLivrosPorGeneroAsync(string genero)
    {
        var livros = await _livroRepository.BuscarPorGeneroAsync(genero);
        if (livros.Any())
        {
            livros.ForEach(l => Console.WriteLine(l));
        }
        else
        {
            Console.WriteLine("Nenhum livro encontrado para o gênero.");
        }
    }
}

// UI: Simulação de interface com o usuário no Console
public class BibliotecaUI
{
    private readonly BibliotecaService _bibliotecaService;

    public BibliotecaUI(BibliotecaService bibliotecaService)
    {
        _bibliotecaService = bibliotecaService;
    }

    public async Task ExibirMenuAsync()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("===== Sistema de Gerenciamento de Biblioteca =====");
            Console.WriteLine("1. Listar livros");
            Console.WriteLine("2. Adicionar livro");
            Console.WriteLine("3. Atualizar livro");
            Console.WriteLine("4. Excluir livro");
            Console.WriteLine("5. Buscar livros por autor");
            Console.WriteLine("6. Buscar livros por gênero");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha uma opção: ");
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    await _bibliotecaService.ListarLivrosAsync();
                    break;
                case 2:
                    await AdicionarLivroAsync();
                    break;
                case 3:
                    await AtualizarLivroAsync();
                    break;
                case 4:
                    await ExcluirLivroAsync();
                    break;
                case 5:
                    await BuscarLivrosPorAutorAsync();
                    break;
                case 6:
                    await BuscarLivrosPorGeneroAsync();
                    break;
                case 0:
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            if (opcao != 0)
            {
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }

        } while (opcao != 0);
    }

    private async Task AdicionarLivroAsync()
    {
        Console.Write("Título: ");
        var titulo = Console.ReadLine();
        Console.Write("Autor: ");
        var autor = Console.ReadLine();
        Console.Write("Gênero: ");
        var genero = Console.ReadLine();
        Console.Write("Ano de publicação: ");
        var anoPublicacao = int.Parse(Console.ReadLine());

        await _bibliotecaService.AdicionarLivroAsync(titulo, autor, genero, anoPublicacao);
    }

    private async Task AtualizarLivroAsync()
    {
        Console.Write("ID do livro a ser atualizado: ");
        var id = int.Parse(Console.ReadLine());
        Console.Write("Novo título: ");
        var titulo = Console.ReadLine();
        Console.Write("Novo autor: ");
        var autor = Console.ReadLine();
        Console.Write("Novo gênero: ");
        var genero = Console.ReadLine();
        Console.Write("Novo ano de publicação: ");
        var anoPublicacao = int.Parse(Console.ReadLine());

        await _bibliotecaService.AtualizarLivroAsync(id, titulo, autor, genero, anoPublicacao);
    }

    private async Task ExcluirLivroAsync()
    {
        Console.Write("ID do livro a ser excluído: ");
        var id = int.Parse(Console.ReadLine());

        await _bibliotecaService.ExcluirLivroAsync(id);
    }

    private async Task BuscarLivrosPorAutorAsync()
    {
        Console.Write("Autor: ");
        var autor = Console.ReadLine();

        await _bibliotecaService.BuscarLivrosPorAutorAsync(autor);
    }

    private async Task BuscarLivrosPorGeneroAsync()
    {
        Console.Write("Gênero: ");
        var genero = Console.ReadLine();

        await _bibliotecaService.BuscarLivrosPorGeneroAsync(genero);
    }
}

// Classe principal com Injeção de Dependência
class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddLogging(builder => builder.AddConsole())
            .AddSingleton<ILivroRepository, LivroRepository>()
            .AddSingleton<BibliotecaService>()
            .AddSingleton<BibliotecaUI>()
            .BuildServiceProvider();

        var bibliotecaUI = serviceProvider.GetService<BibliotecaUI>();
        await bibliotecaUI.ExibirMenuAsync();
    }
}