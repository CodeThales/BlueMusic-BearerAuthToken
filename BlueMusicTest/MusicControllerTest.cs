using BlueMusicBearerAutToken.API;
using BlueMusicBearerAutToken.Controllers;
using BlueMusicBearerAutToken.Models;
using BlueMusicBearerAutToken.Services;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
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
    }
}
