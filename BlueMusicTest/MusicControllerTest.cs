using BlueMusicBearerAutToken.API;
using BlueMusicBearerAutToken.Controllers;
using BlueMusicBearerAutToken.Models;
using BlueMusicBearerAutToken.Services;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace BlueMusicTest
{
    public class MusicControllerTest
    {

        int MusicQuantity = 10;
        List<Music> fakeMusics;

        public MusicControllerTest()
        {
            fakeMusics = new List<Music>();
            for (var i = 1; i <= MusicQuantity; i++)
                fakeMusics.Add(new Music { Id = 1, Name = $"Music {i}" });
        }

        [Fact]
        public void GetMusics_Returns_The_Correct_Musics()
        {
            var musicService = A.Fake<IMusicService>();
            A.CallTo(() => musicService.All()).Returns(fakeMusics);
            var controller = new MusicController(musicService);

            OkObjectResult result = controller.Index() as OkObjectResult;

            var values = result.Value as APIResponse<List<Music>>;
            Assert.True(
                values.Results == fakeMusics &&
                values.Message == "" &&
                values.Succeed
                );
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(0, "Música não encontrada.", false)]
        [InlineData(20, "Música não encontrada.", false)]
        [InlineData(873, "Música não encontrada.", false)]
        [InlineData(-30, "Música não encontrada.", false)]
        [InlineData(null, "Música não encontrada.", false)]        
        public void GetMusic_Return_Music_By_Id(int? id, string message = "", bool succeed = true)
        {
            var musicService = A.Fake<IMusicService>();
            A.CallTo(() => musicService.Get(id)).Returns(fakeMusics.Find(m => m.Id == id));
            var controller = new MusicController(musicService);

            ObjectResult result = controller.Index(id) as ObjectResult;

            var exists = fakeMusics.Find(m => m.Id == id) != null;

            if (exists)
            {
                var values = result.Value as APIResponse<Music>;
                Assert.True(
                    values.Results == fakeMusics.Find(m => m.Id == id) &&
                    values.Message == message &&
                    values.Succeed == succeed
                    );
            }
            else
            {
                var values = result.Value as APIResponse<string>;
                Assert.True(
                    values.Results == null &&
                    values.Message == message &&
                    values.Succeed == succeed
                    );
            }            
        }
    }
}
