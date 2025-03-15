using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RpgApi.Enuns;
using RpgApi.Models;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonagensExercicioController : ControllerBase
    {
        private static List<Personagem> personagens = new List<Personagem>()
        {   
            // Modo de criação e inclusão de objetos de uma só vez na lista
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }
        };

        [HttpGet("GetByNome/{nome}")]
        public IActionResult GetByNome(string nome)
        {
            var personagem = personagens.FirstOrDefault(pe => pe.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

            if (personagem == null)
            {
                return NotFound("Personagem não encontrado");
            }

            return Ok(personagem);
        }

        [HttpGet("GetClerigoMago")]
        public IActionResult GetClerigoMago()
        {
            var personagemCM = personagens.Where(pe => pe.Classe != ClasseEnum.Cavaleiro).OrderByDescending(pe => pe.PontosVida).ToList();

            return Ok(personagemCM);
        }

        [HttpGet("GetEstatisticas")]
        public IActionResult GetEstatisticas()
        {
            var totalPersonagem = personagens.Count();
            var somaEstatistica = personagens.Sum(pe => pe.Inteligencia);

            var exibir = new
            {
                totalPersonagem, somaEstatistica    
            };
            return Ok(exibir);
        }

        [HttpPost("PostValidacao")]
        public IActionResult AddPersonagem(/*[FromBody]*/Personagem novoPersonagem)
        {
            if (novoPersonagem.Defesa < 10 || novoPersonagem.Inteligencia > 30)
            {
                return BadRequest("Defesa precisa ser maior que 10 e inteligência menor que 30");
            }

            personagens.Add(novoPersonagem);
            return Ok(personagens);
        }

        [HttpPost("PostValidacaoMago")]
        public IActionResult AddPersonagemReqMago(/*[FromBody]*/Personagem novoPersonagem)
        {
            if (novoPersonagem.Classe == ClasseEnum.Mago && novoPersonagem.Inteligencia < 35)
            {
                return BadRequest("Mago precisa ter inteligência maior que 30");
            }

            personagens.Add(novoPersonagem);
            return Ok(personagens);
        }


        [HttpGet("GetByClasse/{classeId}")]
        public IActionResult GetByClasseId(int classeId)
        {
            var personagensClasse = personagens.Where(pe => (int)pe.Classe == classeId).ToList();

            if (!personagensClasse.Any())
            {
            return NotFound("Nenhum personagem encontrado para a classe informada");
            }

            return Ok(personagensClasse);
        }

    }
}