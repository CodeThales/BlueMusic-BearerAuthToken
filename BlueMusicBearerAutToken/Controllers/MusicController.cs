using BlueMusicBearerAutToken.API;
using BlueMusicBearerAutToken.Models;
using BlueMusicBearerAutToken.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;

namespace BlueMusicBearerAutToken.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [AuthorizeRoles(RoleType.Admin, RoleType.Common)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class MusicController : ApiBaseController
    {
        IMusicService _service;
        //IMusicService _staticService;

        public MusicController(IMusicService service)
        {
            _service = service;
        }


        /// <summary>
        /// Returns a list of all songs registered in database
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Index() => ApiOk(_service.All());


        /// <summary>
        /// Returns a especific song according to Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Index(int? id) =>
            _service.Get(id) == null ?
            ApiNotFound("Música não encontrada.") :
            ApiOk(_service.Get(id));


        /// <summary>
        /// Returns a song from the database at random
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        [Route("random")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Random()
        {
            var lista = _service.All();
            Random rand = new();
            return ApiOk(lista[rand.Next(lista.Count)]);
        }


        /// <summary>
        /// Creates a specific song according to the values ​​passed in the request body
        /// </summary>
        /// <param name="music"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Create([FromBody] Music music)
        {
            music.CreatedById = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return _service.Create(music) ?
               ApiOk("Música inserida com sucesso.") :
               ApiNotFound("Erro ao inserir música.");
        }


        /// <summary>
        /// Updates a specific song according to the values ​​passed in the request body
        /// </summary>
        /// <param name="music"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Update([FromBody] Music music)
        {
            music.UpdatedById = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return _service.Update(music) ?
                ApiOk("Música atualizada com sucesso.") :
                ApiNotFound("Erro ao atualizar música.");
        }


        /// <summary>
        /// Deletes a specific song according to Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [AuthorizeRoles(RoleType.Admin)]       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Delete(int? id) =>
            _service.Delete(id) ?
            ApiOk("Música excluída com sucesso.") :
            ApiNotFound("Erro ao excluir música.");


        /// <summary>
        /// Returns all songs created by a specific role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("MusicsByRole/{role?}")]        
        public IActionResult MusicsByRole(string role)
        {
            return ApiOk(_service.MusicsByUserRole(role));
        }
    }
}
