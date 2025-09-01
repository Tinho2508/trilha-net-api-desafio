using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            // Busca a tarefa no banco de dados pelo ID fornecido
            var tarefa = _context.Tarefas.Find(id);

            // Valida se a tarefa foi encontrada. Se não, retorna NotFound.
            if (tarefa == null)
                return NotFound();
            
            // Se encontrou, retorna OK com os dados da tarefa
            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            // Busca todas as tarefas no banco de dados e as converte para uma lista
            var tarefas = _context.Tarefas.ToList();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // Busca tarefas cujo título contenha a string recebida
            var tarefas = _context.Tarefas.Where(x => x.Titulo.Contains(titulo)).ToList();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            // Busca tarefas que correspondam exatamente à data fornecida (ignorando a hora)
            var tarefas = _context.Tarefas.Where(x => x.Data.Date == data.Date).ToList();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            // Busca tarefas que correspondam ao status fornecido
            var tarefas = _context.Tarefas.Where(x => x.Status == status).ToList();
            return Ok(tarefas);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // Adiciona a nova tarefa ao contexto do Entity Framework
            _context.Add(tarefa);
            // Salva as mudanças no banco de dados
            _context.SaveChanges();

            // Retorna o status 201 Created com a localização e os dados da nova tarefa
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // Atualiza as propriedades da tarefa encontrada no banco com os dados recebidos
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;
            
            // Salva as mudanças no banco de dados
            _context.SaveChanges();

            // Retorna OK com os dados da tarefa atualizada
            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            // Remove a tarefa do contexto do Entity Framework
            _context.Tarefas.Remove(tarefaBanco);
            // Salva as mudanças (efetiva a exclusão) no banco de dados
            _context.SaveChanges();

            // Retorna o status 204 No Content, indicando sucesso na remoção
            return NoContent();
        }
    }
}
