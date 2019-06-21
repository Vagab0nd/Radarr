using FluentAssertions;
using NUnit.Framework;
using NzbDrone.Core.Test.Framework;

namespace NzbDrone.Core.Test.ParserTests
{

    [TestFixture]
    public class AnimeMetadataParserFixture : CoreTest
    {
        [TestCase("[SubDESU]_High_School_DxD_07_(1280x720_x264-AAC)_[6B7FD717]", "SubDESU", "6B7FD717")]
        [TestCase("[Chihiro]_Working!!_-_06_[848x480_H.264_AAC][859EEAFA]", "Chihiro", "859EEAFA")]
        [TestCase("[Underwater]_Rinne_no_Lagrange_-_12_(720p)_[5C7BC4F9]", "Underwater", "5C7BC4F9")]
        [TestCase("[HorribleSubs]_Hunter_X_Hunter_-_33_[720p]", "HorribleSubs", "")]
        [TestCase("[HorribleSubs] Tonari no Kaibutsu-kun - 13 [1080p].mkv", "HorribleSubs", "")]
        [TestCase("[Doremi].Yes.Pretty.Cure.5.Go.Go!.31.[1280x720].[C65D4B1F].mkv", "Doremi", "C65D4B1F")]
        [TestCase("[Doremi].Yes.Pretty.Cure.5.Go.Go!.31.[1280x720].[C65D4B1F]", "Doremi", "C65D4B1F")]
        [TestCase("[Doremi].Yes.Pretty.Cure.5.Go.Go!.31.[1280x720].mkv", "Doremi", "")]
        [TestCase("[K-F] One Piece 214", "K-F", "")]
        [TestCase("[K-F] One Piece S10E14 214", "K-F", "")]
        [TestCase("[K-F] One Piece 10x14 214", "K-F", "")]
        [TestCase("[K-F] One Piece 214 10x14", "K-F", "")]
        [TestCase("Bleach - 031 - The Resolution to Kill [Lunar].avi", "Lunar", "")]
        [TestCase("[ACX]Hack Sign 01 Role Play [Kosaka] [9C57891E].mkv", "ACX", "9C57891E")]
        [TestCase("[S-T-D] Soul Eater Not! - 06 (1280x720 10bit AAC) [59B3F2EA].mkv", "S-T-D", "59B3F2EA")]
        public void should_parse_absolute_numbers(string postTitle, string subGroup, string hash)
        {
            var result = Parser.Parser.ParseTitle(postTitle);
            result.Should().NotBeNull();
            result.ReleaseGroup.Should().Be(subGroup);
            result.ReleaseHash.Should().Be(hash);
        }

        [TestCase("[PAR] I Want to Eat Your Pancreas (Kimi no Suizou wo Tabetai) [BD 720p AAC] [B9EAC7FF].mkv", "I Want to Eat Your Pancreas (Kimi no Suizou wo Tabetai)", "PAR", "B9EAC7FF")]
        [TestCase("[UTW - THORA] Evangelion 2.22 - You Can(Not) Advance[BD][1080p, x264, DTS - ES]", "Evangelion 2 22 - You Can(Not) Advance", "UTW - THORA", "")]
        [TestCase("Berserk_Golden_Age_Arc_I_Egg_of_the_Supreme_Ruler_(2012)_[720p,BluRay,flac,x264]_-_Taka-THORA", "Berserk Golden Age Arc I Egg of the Supreme Ruler", "THORA", "")]
        [TestCase("I Want to Eat Your Pancreas (Kimi no Suizo wo Tabetai) (2018) [BD 720p Hi10P 5.1 AAC][kuchikirukia].mkv", "I Want to Eat Your Pancreas (Kimi no Suizo wo Tabetai)", null, "")]
        [TestCase("I Want to Eat Your Pancreas [H.264] [720p] [MP4]", "I Want to Eat Your Pancreas", null, "")]
        public void should_parse_movie_title_with_release_group(string postTitle, string title, string subGroup, string hash)
        {
            var result = Parser.Parser.ParseMovieTitle(postTitle, true);
            result.Should().NotBeNull();
            result.ReleaseGroup.Should().Be(subGroup);
            result.ReleaseHash.Should().Be(hash);
            result.MovieTitle.Should().Be(title);
        }
    }
}
