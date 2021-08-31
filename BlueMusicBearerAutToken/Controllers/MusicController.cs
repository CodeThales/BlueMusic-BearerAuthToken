using BlueMusicBearerAutToken.API;
using BlueMusicBearerAutToken.Models;
using BlueMusicBearerAutToken.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace BlueMusicBearerAutToken.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AuthorizeRoles(RoleType.Admin, RoleType.Common)]
    public class MusicController : ApiBaseController
    {
        IMusicService _service;
        //IMusicService _staticService;

        public MusicController(IMusicService service)
        {
            _service = service;
        }


        [HttpGet]
        public IActionResult Index() => ApiOk(_service.All());

                
        [Route("{id}")]
        [HttpGet]
        public IActionResult Index(int? id) =>
            _service.Get(id) == null ?
            ApiNotFound("Música não encontrada.") :
            ApiOk(_service.Get(id));

                
        [Route("random")]
        [HttpGet]
        public IActionResult Random()
        {
            var lista = _service.All();
            Random rand = new();
            return ApiOk(lista[rand.Next(lista.Count)]);
        }

        
        [HttpPost]
        public IActionResult Create([FromBody] Music music)
        {
            music.CreatedById = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return _service.Create(music) ?
               ApiOk("Música inserida com sucesso.") :
               ApiNotFound("Erro ao inserir música.");
        }               


        [HttpPut]
        public IActionResult Update([FromBody] Music music)
        {
            music.UpdatedById = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return _service.Update(music) ?
                ApiOk("Música atualizada com sucesso.") :
                ApiNotFound("Erro ao atualizar música.");
        }            


        [AuthorizeRoles(RoleType.Admin)]
        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int? id) =>
            _service.Delete(id) ?
            ApiOk("Música excluída com sucesso.") :
            ApiNotFound("Erro ao excluir música.");


        [AllowAnonymous]
        [Route("MusicsByRole/{role?}")]
        [HttpGet]
        public IActionResult MusicsByRole(string role)
        {
            return ApiOk(_service.MusicsByUserRole(role));
        }
    }
}
