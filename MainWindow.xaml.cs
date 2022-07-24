using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Extend;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TrackLayeringNFT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public string[] TRACK_FOLDERS = { @"C:\Dev\Kapsule_old\TrackGenerator\FINAL_STEMS\Bass", @"C:\Dev\Kapsule_old\TrackGenerator\FINAL_STEMS\Drums", @"C:\Dev\Kapsule_old\TrackGenerator\FINAL_STEMS\FX", @"C:\Dev\Kapsule_old\TrackGenerator\FINAL_STEMS\Lead" , @"C:\Dev\Kapsule_old\TrackGenerator\FINAL_STEMS\Vocals" };
        public string[] IMAGE_FOLDERS = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\2 - Body_",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\3 - TSP",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\4 - Upper",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\5 - Footwear",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\6 - Lower",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\7 - Eyewear",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\8 - Accessories",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\9 - Jewellery",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\10 - Headwear",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\11 - Earrings"};

        public string[] Faces = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\3 - TSP\1 - Matt",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\3 - TSP\2 - Gloss",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\3 - TSP\3 - Mirror",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\3 - TSP\4 - Specials" };
        public string[] Bodies = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\2 - Body_\1 - Matt Body",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\2 - Body_\2 - Gloss Body",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\2 - Body_\3 - Mirror Body",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\2 - Body_\4 - Special Body" };
        public string[] TSP = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\3 - TSP\1 - Matt",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\3 - TSP\2 - Gloss",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\3 - TSP\3 - Mirror",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\3 - TSP\4 - Specials" };
        public string[] JEWELLERY_FOLDERS = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\9 - Jewellery\1 - Chain",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\9 - Jewellery\2 - BTC Chain",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\9 - Jewellery\3 - ETH Chain",
            @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\9 - Jewellery\4 - STP Chain"};
        public string[] UPPER_FOLDERS = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\4 - Upper\1 - Crewneck",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\4 - Upper\2 - Hoodie",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\4 - Upper\3 - Essential",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\4 - Upper\4 - Zip Hoodie",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\4 - Upper\5 - Varsity",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\4 - Upper\6 - Tank Top",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\4 - Upper\7 - Puffer"};
        public string[] LOWER_FOLDERS = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\6 - Lower\1 - Trek Shorts",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\6 - Lower\2 - Track Pants"};
        public string[] EARRINGS_FOLDERS = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\11 - Earrings\1 - Small Hoop",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\11 - Earrings\2 - Square Hoop",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\11 - Earrings\3 - Multi Stud"};
        public string[] ACCESSORIES_FOLDERS = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\8 - Accessories\1 - Boom box",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\8 - Accessories\2 - Flash light",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\8 - Accessories\3 - Brand Bag",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\8 - Accessories\4 - Baseball Bat",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\8 - Accessories\5 - Fishing Rod"};
        public string[] EYEWEAR_FOLDERS = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\7 - Eyewear\1 - Snow",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\7 - Eyewear\2 - LensWrap",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\7 - Eyewear\3 - Shutter",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\7 - Eyewear\4 - Pixel",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\7 - Eyewear\5 - 3D",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\7 - Eyewear\6 - Cyclops",
                                         @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\7 - Eyewear\7 - Monacle"};
        public string[] HEADWEAR_FOLDERS = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\10 - Headwear" };
        public string[] FOOTWEAR_FOLDERS = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\5 - Footwear" };
        public string[] BACKGROUND_FOLDERS = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\1 - Background" };
        public string[] RUDEBOY_BACKGROUNDS = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\12 - Rudeboy\Backgrounds" };
        public string[] RUDEBOY_BADGE = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\12 - Rudeboy\Badge" };
        public string[] RUDEBOY_EYES = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\12 - Rudeboy\Eyes" };
        public string[] RUDEBOY_FACE = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\12 - Rudeboy\Face" };
        public string[] RUDEBOY_HEAD = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\12 - Rudeboy\Head" };
        public string[] RUDEBOY_MOUTH = { @"C:\Dev\Kapsule_old\TrackGenerator\TSP LAyers\12 - Rudeboy\Mouth" };

        public string[][] m_AllBackgrounds = null;
        public string[][] m_AllJewellery = null;
        public string[][] m_AllUpper = null;
        public string[][] m_AllLower = null;
        public string[][] m_AllEarrings= null;
        public string[][] m_AllAccessories= null;
        public string[][] m_AllEyewear= null;
        public string[][] m_AllHeadwear = null;
        public string[][] m_AllFootwear = null;
        public string[][] m_AllTracks = null;
        public string[][] m_AllImages = null;
        public string[][] m_AllFaces = null;
        public string[][] m_AllBodies = null;
        public string[][] m_AllTSP = null;
        public string[][] m_AllRudeboyMouth = null;
        public string[][] m_AllRudeboyBackground = null;
        public string[][] m_AllRudeboyBadge = null;
        public string[][] m_AllRudeboyEyes = null;
        public string[][] m_AllRudeboyFace = null;
        public string[][] m_AllRudeboyHead = null;

        public string[][][] m_AllLayers = null;

        List<int> specialIndicies = new List<int> {
97   ,
105  ,
212  ,
252  ,
253  ,
405  ,
493  ,
564  ,
621  ,
724  ,
737  ,
815  ,
863  ,
870  ,
895  ,
969  ,
970  ,
1022 ,
1104 ,
1210 ,
1439 ,
1472 ,
1577 ,
1821 ,
1836 ,
1841 ,
2306 ,
2321 ,
2345 ,
2448 ,
2575 ,
2603 ,
2632 ,
2652 ,
2968 ,
2988 ,
3082 ,
3352 ,
3608 ,
3627 ,
3906 ,
3983 ,
4125 ,
4302 ,
4308 ,
4488 ,
4661 ,
4729 ,
4791 ,
4891 ,
1157 ,
1605 ,
1255 ,
3408 ,
4526 ,
4385 ,
1084 ,
2922 ,
1760 ,
3933 ,
4552 ,
2976 ,
790  ,
1462 ,
2840 ,
4897 ,
3212 ,
4596 ,
4544 ,
4060 ,
1883 ,
4510 ,
1657 ,
1948 ,
1319 ,
2716 ,
4212 ,
813  ,
2461 ,
2526 ,
1942 ,
1033 ,
4805 ,
1980 ,
3079 ,
4025 ,
279  ,
843  ,
4630 ,
366  ,
4443 ,
604  ,
850  ,
3566 ,
2964 ,
1032 ,
376  ,
2807 ,
4907 ,
1889 ,
390  ,
1216 ,
749  ,
4820 ,
658  ,
2029 ,
1656 ,
1857 ,
4494 ,
93   ,
2896 ,
1878 ,
1263 ,
95   ,
1839 ,
714  ,
3390 ,
3187 ,
4351 ,
2351 ,
812  ,
393  ,
1578 ,
4258 ,
3057 ,
4757 ,
1065 ,
2638 ,
2151 ,
4283 ,
1484 ,
4154 ,
85   ,
432  ,
1719 ,
3571 ,
510  ,
3966 ,
2779 ,
3422 ,
2440 ,
4508 ,
2740 ,
1676 ,
2154 ,
2138 ,
2502 ,
1528 ,
3195 ,
1305 ,
4632 ,
3086 ,
1863 ,
1507 ,
267  ,
4448 ,
939  ,
3903 ,
3301 ,
1405 ,
2815 ,
3053 ,
4410 ,
2261 ,
3513 ,
4898 ,
3951 ,
2643 ,
4748 ,
1983 ,
286  ,
1392 ,
624  ,
2197 ,
2404 ,
3413 ,
2920 ,
3899 ,
1838 ,
1271 ,
2107 ,
103  ,
3199 ,
2012 ,
968  ,
136  ,
4933 ,
1670 ,
3554 ,
2683 ,
213  ,
4795 ,
3134 ,
2804 ,
3100 ,
3572 ,
2568 ,
1978 ,
1338 ,
1011 ,
261  ,
3440 ,
3826 ,
3596 ,
3997 ,
4338 ,
2686 ,
4798 ,
3186 ,
4851 ,
4028 ,
4883 ,
2220 ,
2960 ,
3796 ,
542  ,
4742 ,
821  ,
3662 ,
4868 ,
868  ,
4229 ,
152  ,
2223 ,
3278 ,
3392 ,
2224 ,
3014 ,
2589 ,
338  ,
4024 ,
2416 ,
2793 ,
1491 ,
1746 ,
4470 ,
4056 ,
4487 ,
4138 ,
2095 ,
3467 ,
2389 ,
2119 ,
4610 ,
1530 ,
4660 ,
809  ,
4969 ,
4061 ,
4639 ,
4533 ,
1683 ,
407  ,
1324 ,
4030 ,
3533 ,
3465 ,
956  ,
1071 ,
1311 ,
3961 ,
2471 ,
4128 ,
2864 ,
2365 ,
867  ,
1383 ,
3926 ,
241  ,
4478 ,
1010 ,
4409 ,
4430 ,
3757 ,
1909 ,
4801 ,
540  ,
994  ,
3956 ,
643  ,
4706 ,
2593 ,
1345 ,
2369 ,
4153 ,
3470 ,
2150 ,
4489 ,
1807 ,
4582 ,
3152 ,
1785 ,
2500 ,
4609 ,
4694 ,
3145 ,
1023 ,
4353 ,
3944 ,
3405 ,
2703 ,
2924 ,
79   ,
1344 ,
1781 ,
2226 ,
3879 ,
3733 ,
3748 ,
3233 ,
3885 ,
4674 ,
710  ,
4344 ,
2483 ,
3641 ,
1446 ,
1019 ,
4545 ,
1260 ,
1738 ,
4824 ,
3088 ,
4214 ,
656  ,
4239 ,
2491 ,
744  ,
2333 ,
2399 ,
4944 ,
245  ,
2435 ,
552  ,
1707 ,
497  ,
2140 ,
2303 ,
4357 ,
2308 ,
508  ,
4431 ,
960  ,
2508 ,
4327 ,
4774 ,
4425 ,
343  ,
991  ,
4394 ,
1851 ,
4518 ,
1935 ,
3317 ,
2828 ,
2553 ,
3654 ,
4773 ,
4943 ,
2137 ,
460  ,
1200 ,
3583 ,
434  ,
3164 ,
4090 ,
4381 ,
334  ,
3562 ,
684  ,
4241 ,
4530 ,
928  ,
2271 ,
185  ,
4699 ,
340  ,
652  ,
3862 ,
2009 ,
82   ,
4426 ,
4763 ,
3664 ,
314  ,
2949 ,
328  ,
613  ,
766  ,
3156 ,
4935 ,
2887 ,
3651 ,
4068 ,
580  ,
2044 ,
865  ,
2961 ,
4600 ,
569  ,
2275 ,
1406 ,
2115 ,
4116 ,
3668 ,
2313 ,
4812 ,
4417 ,
1774 ,
2465 ,
1427 ,
2460 ,
3036 ,
764  ,
4091 ,
481  ,
2796 ,
2527 ,
2287 ,
3361 ,
607  ,
4855 ,
4010 ,
1622 ,
4225 ,
4728 ,
1860 ,
633  ,
3986 ,
3379 ,
3114 ,
4501 ,
2069 ,
1627 ,
3331 ,
1555 ,
3528 ,
3383 ,
2602 ,
3969 ,
2062 ,
1297 ,
4766 ,
1102 ,
740  ,
4707 ,
1865 ,
2142 ,
4747 ,
4119 ,
690  ,
1043 ,
220  ,
4178 ,
4715 ,
4665 ,
3179 ,
4967 ,
1700 ,
3564 ,
304  ,
2318 ,
203  ,
        }; 
        public System.Random randGenerator = new System.Random(696969);
        public MainWindow()
        {
            InitializeComponent();

        }

        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {

        }
        //SELECT FOLDER
        /*
        using (var fbd = new FolderBrowserDialog())
        {
            DialogResult result = fbd.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                string[] files = Directory.GetFiles(fbd.SelectedPath);

                System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
            }
        }

        m_AllTracks = new string[TRACK_FOLDERS.Length][];
        for (int i = 0; i < TRACK_FOLDERS.Length; i++)
        {
            string folder = TRACK_FOLDERS[i];
            m_AllTracks[i] = Directory.GetFiles(folder);
        }

        for (int a = 0; a < m_AllTracks[0].Length; a++)
            for (int b = 0; b < m_AllTracks[1].Length; b++)
                for (int c = 0; c < m_AllTracks[2].Length; c++)
                    for (int d = 0; d < m_AllTracks[3].Length; d++)
                {
                    using (var readerA = new AudioFileReader(m_AllTracks[0][a]))
                    using (var readerB = new AudioFileReader(m_AllTracks[1][b]))
                    using (var readerC = new AudioFileReader(m_AllTracks[2][c]))
                    using (var readerD = new AudioFileReader(m_AllTracks[3][d]))
                    {
                        var mixer = new MixingSampleProvider(new[] { readerA, readerB, readerC , readerD });
                        WaveFileWriter.CreateWaveFile16($"a{a}b{b}c{c}d{d}.wav", mixer);
                        //WaveFileWriter.CreateWaveFile16($"a{a}b{b}c{c}.wav", mixer);
                    }
                }
                   */

        private void Generate_Click(object sender, RoutedEventArgs ev)
        {
            OldInitFolders();

            #region MUSIC

            m_AllTracks = new string[TRACK_FOLDERS.Length][];
            for (int i = 0; i < TRACK_FOLDERS.Length; i++)
            {
                string folder = TRACK_FOLDERS[i];
                m_AllTracks[i] = Directory.GetFiles(folder);
            }
            #endregion

            /*
            string inputImage = @"C:\Users\DireStrait\Downloads\TSP_0.png";
            string inputAudioFile = @"C:\Users\DireStrait\Downloads\a4b0c1d0.wav";
            string outputVideoFile = @"C:\Dev\preview.webm";

            bool success = FFMpeg.PosterWithAudio(inputImage, inputAudioFile, outputVideoFile);
            */
            System.Random seed = new System.Random();
            System.Random rand = new System.Random(seed.Next());

            ThreadPool.SetMaxThreads(2, 2);

            var tasks = new List<Task>();

            string[] files = Directory.GetFiles(@"C:\Dev\Kapsule_old\TrackGenerator\TrackLayeringNFT\bin\Debug\metadata");
            for (int i = 0; i < files.Length; ++i)
            {
                string file = files[i];
                string mp4FileName = Path.Combine(@"C:\Dev\Kapsule_old\TrackGenerator\TrackLayeringNFT\bin\Debug", $"{Path.GetFileNameWithoutExtension(file)}.mp4");

                if (File.Exists(Path.Combine(@"C:\Dev\Kapsule_old\TrackGenerator\TrackLayeringNFT\bin\Debug", $"{Path.GetFileName(file)}")))
                {
                    Console.WriteLine($"Deleted old {mp4FileName}");
                    File.Delete(mp4FileName);
                }
                var stringMetadata = File.ReadAllText(Path.Combine(file));
                Metadata metadata = JsonConvert.DeserializeObject<Metadata>(stringMetadata);

                string fileName = Path.GetFileNameWithoutExtension(file);

                metadata.name = $"{fileName}";
                metadata.description = $"Toys, but for big kids. We're taking you on a journey to reconnect with your inner child. Stickmen Toys is a collection of 5,000 utility-enabled, audio-visual NFTs by Warner Records UK and Bose. Ownership of a Stickmen Toy gifts collectors with curated rewards from brand partners, exclusive merchandise, as well as future Warner Records NFT drops. Welcome to our playground.";
                metadata.mp4 = $"{fileName}.mp4";
                string inputImage = Path.Combine(@"C:\Dev\Kapsule_old\TrackGenerator\TrackLayeringNFT\bin\Debug\images", $"{fileName}.png");
                string inputAudioFile = Path.Combine(@"C:\Dev\Kapsule_old\TrackGenerator\TrackLayeringNFT\bin\Debug\tracks", $"{fileName}.wav");
                string outputVideoFile = $"{fileName}.mp4";

                string args = $"-loop 1 -i {Path.Combine(@"C:\Dev\Kapsule_old\TrackGenerator\TrackLayeringNFT\bin\Debug\images\", $"{Path.GetFileNameWithoutExtension(file)}.png")} -i {Path.Combine(@"C:\Dev\Kapsule_old\TrackGenerator\TrackLayeringNFT\bin\Debug\tracks\", $"{Path.GetFileNameWithoutExtension(file)}.wav")} -c:v libx264 -t 30.5 -pix_fmt yuv420p {mp4FileName} -threads 0";

                tasks.Add(new Task(() =>
              {
                  System.Diagnostics.Process proc = System.Diagnostics.Process.Start($"ffmpeg.exe", args);
                  proc.WaitForExit();

                  System.Console.WriteLine($"Produced:{fileName}.mp4");

                  string jsonString = JsonConvert.SerializeObject(metadata);
                  using (FileStream metadataFile = File.Create(Path.Combine(@"C:\Dev\Kapsule_old\TrackGenerator\TrackLayeringNFT\bin\Debug", $"{fileName}.json")))
                  {
                      byte[] jsonBytes = Encoding.Default.GetBytes(jsonString);
                      metadataFile.Write(jsonBytes, 0, jsonBytes.Length);
                      System.Console.WriteLine($"Produced:{Path.GetFileName(file)} => {fileName}.json");
                  }
              }));
            }

            LimitedConcurrencyLevelTaskScheduler scheduler = new LimitedConcurrencyLevelTaskScheduler(1);

            foreach(Task task in tasks)
            {
                task.Start(scheduler);
            }

            Task.WaitAll(tasks.ToArray());
            foreach (int indice in specialIndicies) 
            {
                Console.WriteLine(indice);
            }

            return;

            for (int j = 100; j < 350; ++j)
            {

                int nftIndex = specialIndicies[j];
                Metadata metadata = new Metadata();

                #region MUSIC

                int a = rand.Next(0, m_AllTracks[0].Length);
                int b = rand.Next(0, m_AllTracks[1].Length);
                int c = rand.Next(0, m_AllTracks[2].Length);
                int d = rand.Next(0, m_AllTracks[3].Length);
                int e = rand.Next(0, m_AllTracks[4].Length);

                metadata.attributes.Add(new Metadata.Attribute("Bass", Path.GetFileNameWithoutExtension(m_AllTracks[0][a])));
                metadata.attributes.Add(new Metadata.Attribute("Drums", Path.GetFileNameWithoutExtension(m_AllTracks[1][b])));
                metadata.attributes.Add(new Metadata.Attribute("FX", Path.GetFileNameWithoutExtension(m_AllTracks[2][c])));
                metadata.attributes.Add(new Metadata.Attribute("Lead", Path.GetFileNameWithoutExtension(m_AllTracks[3][d])));
                metadata.attributes.Add(new Metadata.Attribute("Vocals", Path.GetFileNameWithoutExtension(m_AllTracks[4][e])));

                using (var readerA = new AudioFileReader(m_AllTracks[0][a]))
                using (var readerB = new AudioFileReader(m_AllTracks[1][b]))
                using (var readerC = new AudioFileReader(m_AllTracks[2][c]))
                using (var readerD = new AudioFileReader(m_AllTracks[3][d]))
                using (var readerE = new AudioFileReader(m_AllTracks[4][e]))
                {
                    var mixer = new MixingSampleProvider(new[] { readerA, readerB, readerC, readerD, readerE });
                    WaveFileWriter.CreateWaveFile16($"{nftIndex}.wav", mixer);
                }
                #endregion


                List<Image> layerImages = new List<Image>();
                List<string> layerImageFiles = new List<string>();

                SelectBackground(ref metadata, ref layerImageFiles);

                SelectBodyType(ref metadata, ref layerImageFiles);

                SelectTSP(ref metadata, ref layerImageFiles);

                SelectFootwear(ref metadata, ref layerImageFiles);
                SelectLower(ref metadata, ref layerImageFiles);
                SelectUpper(ref metadata, ref layerImageFiles);

                SelectEarrings(ref metadata, ref layerImageFiles);

                //if (!HasAttributeValue(metadata, "Hoodie (black)") &&
                //    !HasAttributeValueSubstring(metadata, "Crewneck") &&
                //    !HasAttributeValueSubstring(metadata, "Essential") &&
                //    !HasAttributeValueSubstring(metadata, "Track") &&
                //    !HasAttributeValueSubstring(metadata, "Warmer") &&
                //    !HasAttributeValueSubstring(metadata, "Hoodie"))
                //    SelectJewellery(ref metadata, ref layerImageFiles);
                
                //SelectEyewear(ref metadata, ref layerImageFiles, HasAttribute(metadata, "Earrings"));

                if (!HasAttributeValueSubstring(metadata, "Hoodie"))
                    SelectAccessories(ref metadata, ref layerImageFiles);

                if (!HasAttributeValue(metadata, "Lens wrap black") &&
                    !HasAttributeValue(metadata, "Lens wrap red") &&
                    !HasAttributeValue(metadata, "Lens wrap silver") &&
                    !HasAttributeValueSubstring(metadata, "Ski") &&
                    !HasAttributeValueSubstring(metadata, "Hoop") &&
                    !HasAttributeValueSubstring(metadata, "Square"))
                    SelectHeadwear(ref metadata, ref layerImageFiles, HasAttribute(metadata, "Eyewear"));
                
                SwapJewelleryAndUpper(metadata, ref layerImageFiles);

                layerImages = GetLayerImages(layerImageFiles);

                var bitmap = new Bitmap(5000, 5000);
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    foreach (var image in layerImages)
                    {
                        graphics.DrawImage(image, 0, 0, 5000, 5000);
                    }
                }
                bitmap.Save($"{nftIndex}.png", System.Drawing.Imaging.ImageFormat.Png);
                bitmap.Dispose();
                System.Console.WriteLine($"Produced: {nftIndex}.png");

                foreach (Image img in layerImages)
                {
                    img.Dispose();
                }
                layerImages.Clear();

                metadata.image = $"{nftIndex}.png";
                metadata.track = $"{nftIndex}.wav";
                metadata.mp4 = "";
                string jsonString = JsonConvert.SerializeObject(metadata);
                using (FileStream metadataFile = File.Create($"{nftIndex}.json"))
                {
                    byte[] jsonBytes = Encoding.Default.GetBytes(jsonString);
                    metadataFile.Write(jsonBytes, 0, jsonBytes.Length);
                    System.Console.WriteLine($"Produced: {nftIndex}.json");
                }
            }

            foreach (int indice in specialIndicies)
            {
                Console.WriteLine(indice);
            }
            return;

            for (int i = 0; i < 5050; ++i)
            {
                Metadata metadata = new Metadata();
                int nftIndex = specialIndicies[i];

                #region MUSIC

                int a = rand.Next(0, m_AllTracks[0].Length);
                int b = rand.Next(0, m_AllTracks[1].Length);
                int c = rand.Next(0, m_AllTracks[2].Length);
                int d = rand.Next(0, m_AllTracks[3].Length);
                int e = rand.Next(0, m_AllTracks[4].Length);

                metadata.attributes.Add(new Metadata.Attribute("Bass",   Path.GetFileNameWithoutExtension(m_AllTracks[0][a])));
                metadata.attributes.Add(new Metadata.Attribute("Drums",  Path.GetFileNameWithoutExtension(m_AllTracks[1][b])));
                metadata.attributes.Add(new Metadata.Attribute("FX",     Path.GetFileNameWithoutExtension(m_AllTracks[2][c])));
                metadata.attributes.Add(new Metadata.Attribute("Lead",   Path.GetFileNameWithoutExtension(m_AllTracks[3][d])));
                metadata.attributes.Add(new Metadata.Attribute("Vocals", Path.GetFileNameWithoutExtension(m_AllTracks[4][e])));

                using (var readerA = new AudioFileReader(m_AllTracks[0][a]))
                using (var readerB = new AudioFileReader(m_AllTracks[1][b]))
                using (var readerC = new AudioFileReader(m_AllTracks[2][c]))
                using (var readerD = new AudioFileReader(m_AllTracks[3][d]))
                using (var readerE = new AudioFileReader(m_AllTracks[4][e]))
                {
                    var mixer = new MixingSampleProvider(new[] { readerA, readerB, readerC, readerD, readerE });
                    WaveFileWriter.CreateWaveFile16($"{i}.wav", mixer);
                }
                #endregion

                List<Image> layerImages = new List<Image>();
                List<string> layerImageFiles = new List<string>();

                SelectBackground(ref metadata, ref layerImageFiles);

                SelectBodyType(ref metadata, ref layerImageFiles);
                
                SelectTSP(ref metadata, ref layerImageFiles);
                SelectFootwear(ref metadata, ref layerImageFiles);
                SelectLower(ref metadata, ref layerImageFiles);
                SelectUpper(ref metadata, ref layerImageFiles);


                SelectEarrings(ref metadata, ref layerImageFiles);

                if (!HasAttributeValue(metadata, "Hoodie (black)") &&
                    !HasAttributeValueSubstring(metadata, "Crewneck") &&
                    !HasAttributeValueSubstring(metadata, "Essential") &&
                    !HasAttributeValueSubstring(metadata, "Track") &&
                    !HasAttributeValueSubstring(metadata, "Hoodie"))
                    SelectJewellery(ref metadata, ref layerImageFiles);

                SelectEyewear(ref metadata, ref layerImageFiles, HasAttribute(metadata, "Earrings"));

                if(!HasAttributeValueSubstring(metadata, "Hoodie"))
                    SelectAccessories(ref metadata, ref layerImageFiles);

                if(!HasAttributeValue(metadata, "Lens wrap black") &&
                    !HasAttributeValue(metadata, "Lens wrap red") &&
                    !HasAttributeValue(metadata, "Lens wrap silver") &&
                    !HasAttributeValueSubstring(metadata, "Ski") && 
                    !HasAttributeValueSubstring(metadata, "Hoop") && 
                    !HasAttributeValueSubstring(metadata, "Square"))
                    SelectHeadwear(ref metadata, ref layerImageFiles, HasAttribute(metadata, "Eyewear"));

                SwapJewelleryAndUpper(metadata, ref layerImageFiles);

                layerImages = GetLayerImages(layerImageFiles);

                var bitmap = new Bitmap(5000, 5000);
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    foreach (var image in layerImages)
                    {
                        graphics.DrawImage(image, 0, 0, 5000, 5000);
                    }
                }
                bitmap.Save($"{nftIndex}.png", System.Drawing.Imaging.ImageFormat.Png);
                bitmap.Dispose();
                System.Console.WriteLine($"Produced: {nftIndex}.png");

                foreach (Image img in layerImages)
                {
                    img.Dispose();
                }
                layerImages.Clear();

                metadata.image = $"{nftIndex}.png";
                metadata.track = $"{nftIndex}.wav";
                metadata.mp4 = "";
                string jsonString = JsonConvert.SerializeObject(metadata);
                using (FileStream metadataFile = File.Create($"{nftIndex}.json"))
                {
                    byte[] jsonBytes = Encoding.Default.GetBytes(jsonString);
                    metadataFile.Write(jsonBytes, 0, jsonBytes.Length);
                    System.Console.WriteLine($"Produced: {nftIndex}.json");
                }
            }


            return;

            /*
            MetadataWindow metadataWindow = new MetadataWindow();
            metadataWindow.Show();

            return;
            */
            
            }

            /*
            List<Metadata> allMetadata = new List<Metadata>();
            List <List<string>> allLayers = new List<List<string>>();
            GenerateMetadata(ref allMetadata, ref allLayers);

            //Dump big files
            string allMetadataString = JsonConvert.SerializeObject(allMetadata);
            string allLayersString = JsonConvert.SerializeObject(allLayers);

            using (FileStream metadataFile = File.Create($"ALL_DATA.json"))
            {
                byte[] jsonBytes = Encoding.Default.GetBytes(allMetadataString);
                byte[] moreBytes = Encoding.Default.GetBytes(allLayersString);
                metadataFile.Write(jsonBytes, 0, jsonBytes.Length);
                metadataFile.Write(moreBytes, 0, moreBytes.Length);
                System.Console.WriteLine($"Produced: ALL_DATA.json");
            }

            for (int i = 0; i < 5000; ++i)
            {
                List<Image> layerImages;

                layerImages = GetLayerImages(allLayers[i]);

                var bitmap = new Bitmap(5000, 5000);
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    foreach (var image in layerImages)
                    {
                        graphics.DrawImage(image, 0, 0, 5000, 5000);
                    }
                }
                bitmap.Save($"TSP_{i}.png", System.Drawing.Imaging.ImageFormat.Png);
                bitmap.Dispose();
                System.Console.WriteLine($"Produced: TSP_{i}.png");

                string jsonString = JsonConvert.SerializeObject(allMetadata[i]);
                using (FileStream metadataFile = File.Create($"TSP_{i}.json"))
                {
                    byte[] jsonBytes = Encoding.Default.GetBytes(jsonString);
                    metadataFile.Write(jsonBytes, 0, jsonBytes.Length);
                    System.Console.WriteLine($"Produced: TSP_{i}.json");
                }

                foreach (Image img in layerImages)
                {
                    img.Dispose();
                }
                layerImages.Clear();
            }
            */
        

        private void InitFolders()
        {
            //SKIP THIS WHOLE FUNCTION AND GO STRAIGHT UP GENERATE METADATA

        }

        private void OldInitFolders()
        {
            m_AllImages = new string[IMAGE_FOLDERS.Length][];
            for (int i = 0; i < IMAGE_FOLDERS.Length; i++)
            {
                string folder = IMAGE_FOLDERS[i];
                m_AllImages[i] = Directory.GetFiles(folder);
            }

            m_AllBodies = new string[Bodies.Length][];
            for (int i = 0; i < Bodies.Length; i++)
            {
                string folder = Bodies[i];
                m_AllBodies[i] = Directory.GetFiles(folder);
            }
            m_AllFaces = new string[Faces.Length][];
            for (int i = 0; i < Faces.Length; i++)
            {
                string folder = Faces[i];
                m_AllFaces[i] = Directory.GetFiles(folder);
            }
            m_AllTSP = new string[TSP.Length][];
            for (int i = 0; i < TSP.Length; i++)
            {
                string folder = TSP[i];
                m_AllTSP[i] = Directory.GetFiles(folder);
            }
            m_AllJewellery = new string[JEWELLERY_FOLDERS.Length][];
            for (int i = 0; i < JEWELLERY_FOLDERS.Length; i++)
            {
                string folder = JEWELLERY_FOLDERS[i];
                m_AllJewellery[i] = Directory.GetFiles(folder);
            }
            m_AllUpper = new string[UPPER_FOLDERS.Length][];
            for (int i = 0; i < UPPER_FOLDERS.Length; i++)
            {
                string folder = UPPER_FOLDERS[i];
                m_AllUpper[i] = Directory.GetFiles(folder);
            }
            m_AllLower = new string[LOWER_FOLDERS.Length][];
            for (int i = 0; i < LOWER_FOLDERS.Length; i++)
            {
                string folder = LOWER_FOLDERS[i];
                m_AllLower[i] = Directory.GetFiles(folder);
            }
            m_AllEarrings = new string[EARRINGS_FOLDERS.Length][];
            for (int i = 0; i < EARRINGS_FOLDERS.Length; i++)
            {
                string folder = EARRINGS_FOLDERS[i];
                m_AllEarrings[i] = Directory.GetFiles(folder);
            }
            m_AllAccessories = new string[ACCESSORIES_FOLDERS.Length][];
            for (int i = 0; i < ACCESSORIES_FOLDERS.Length; i++)
            {
                string folder = ACCESSORIES_FOLDERS[i];
                m_AllAccessories[i] = Directory.GetFiles(folder);
            }
            m_AllEyewear = new string[EYEWEAR_FOLDERS.Length][];
            for (int i = 0; i < EYEWEAR_FOLDERS.Length; i++)
            {
                string folder = EYEWEAR_FOLDERS[i];
                m_AllEyewear[i] = Directory.GetFiles(folder);
            }
            m_AllHeadwear = new string[HEADWEAR_FOLDERS.Length][];
            for (int i = 0; i< HEADWEAR_FOLDERS.Length; i++)
            {
                string folder = HEADWEAR_FOLDERS[i];
                m_AllHeadwear[i] = Directory.GetFiles(folder);
            }
            m_AllFootwear = new string[FOOTWEAR_FOLDERS.Length][];
            for (int i = 0; i < FOOTWEAR_FOLDERS.Length; i++)
            {
                string folder = FOOTWEAR_FOLDERS[i];
                m_AllFootwear[i] = Directory.GetFiles(folder);
            }
            m_AllBackgrounds = new string[BACKGROUND_FOLDERS.Length][];
            for (int i = 0; i < BACKGROUND_FOLDERS.Length; i++)
            {
                string folder = BACKGROUND_FOLDERS[i];
                m_AllBackgrounds[i] = Directory.GetFiles(folder);
            }

            m_AllRudeboyBackground= new string[RUDEBOY_BACKGROUNDS.Length][];
            for (int i = 0; i < RUDEBOY_BACKGROUNDS.Length; i++)
            {
                string folder = RUDEBOY_BACKGROUNDS[i];
                m_AllRudeboyBackground[i] = Directory.GetFiles(folder);
            }            
            m_AllRudeboyBadge= new string[RUDEBOY_BADGE.Length][];
            for (int i = 0; i < RUDEBOY_BADGE.Length; i++)
            {
                string folder = RUDEBOY_BADGE[i];
                m_AllRudeboyBadge[i] = Directory.GetFiles(folder);
            }            
            m_AllRudeboyEyes= new string[RUDEBOY_EYES.Length][];
            for (int i = 0; i < RUDEBOY_EYES.Length; i++)
            {
                string folder = RUDEBOY_EYES[i];
                m_AllRudeboyEyes[i] = Directory.GetFiles(folder);
            }            
            m_AllRudeboyFace= new string[RUDEBOY_FACE.Length][];
            for (int i = 0; i < RUDEBOY_FACE.Length; i++)
            {
                string folder = RUDEBOY_FACE[i];
                m_AllRudeboyFace[i] = Directory.GetFiles(folder);
            }            
            m_AllRudeboyHead= new string[RUDEBOY_HEAD.Length][];
            for (int i = 0; i < RUDEBOY_HEAD.Length; i++)
            {
                string folder = RUDEBOY_HEAD[i];
                m_AllRudeboyHead[i] = Directory.GetFiles(folder);
            }
            m_AllRudeboyMouth = new string[RUDEBOY_MOUTH.Length][];
            for (int i = 0; i < RUDEBOY_MOUTH.Length; i++)
            {
                string folder = RUDEBOY_MOUTH[i];
                m_AllRudeboyMouth[i] = Directory.GetFiles(folder);
            }
        }

        private List<Image> GetLayerImages(List<string> fileNames)
        {
            List<Image> imageLayers = new List<Image>();

            foreach (string fileName in fileNames)
            {
                imageLayers.Add(Image.FromFile(fileName));
            }

            return imageLayers;
        }

        private bool HasAttribute(Metadata metadata, string attribute)
        {
            return metadata.attributes.Exists(y =>
              {
                  return y.trait_type == attribute;
              });
        }
        private bool HasAttributeValue(Metadata metadata, string value)
        {
            return metadata.attributes.Exists(y =>
            {
                return y.value == value;
            });
        }
        private bool HasAttributeValueSubstring(Metadata metadata, string value)
        {
            return metadata.attributes.Exists(y =>
            {
                return y.value.Contains(value);
            });
        }
        private void GenerateMetadata(ref List<Metadata> metadataList, ref List<List<string>> layersList)
        {
            System.Random seed = new System.Random();
            System.Random rand = new System.Random(seed.Next());

            for (int i = 0; i < MetadataGenerator.m_CollectionSize; ++i)
            {
                Metadata metadata = new Metadata();
                List<string> layers = new List<string>();

                List<System.Guid> exclusionList = new List<System.Guid>();
                foreach(MetadataDescriptor desc in MetadataGenerator.m_MetadataDescriptors[0].children)
                {
                    MetadataDescriptor result;
                    bool hasChoice = desc.GetChoiceForCategory(out result, ref rand, ref exclusionList);

                    if (hasChoice)
                        metadata.attributes.Add(new Metadata.Attribute(result.GetType(), result.name));
                }
            }
        }
        private void OldGenerateMetadata(ref List<Metadata> metadataList, ref List<List<string>> layersList)
        {
            for (int i = 0; i < 5000; ++i)
            {
                Metadata metadata = new Metadata();
                List<string> layers = new List<string>();

                SelectBackground(ref metadata, ref layers);
                SelectBodyType(ref metadata, ref layers);
                SelectFace(ref metadata, ref layers);
                SelectTSP(ref metadata, ref layers);
                SelectLower(ref metadata, ref layers);
                //SelectHeadwear(ref metadata, ref layers);
                SelectEyewear(ref metadata, ref layers, true);
                SelectUpper(ref metadata, ref layers);
                SelectJewellery(ref metadata, ref layers);
                SelectEarrings(ref metadata, ref layers);
                SelectAccessories(ref metadata, ref layers);

                SwapJewelleryAndUpper(metadata, ref layers);

                metadataList.Add(metadata);
                layersList.Add(layers);
            }
        }

        private void SwapJewelleryAndUpper(Metadata metadata, ref List<string> layers)
        {
            if (HasAttribute(metadata, "Jewellery") && HasAttribute(metadata, "Upper"))
            {
                bool isHoodie = false;
                bool hasJewellery = false;
                for (int j = 0; j < metadata.attributes.Count; ++j)
                {
                    if (metadata.attributes[j].trait_type.Contains("Upper"))
                    {
                        if (metadata.attributes[j].value.Contains("Varsity") || metadata.attributes[j].value.Contains("Zip") || metadata.attributes[j].value.Contains("Crewneck") || metadata.attributes[j].value.Contains("Boiler") || metadata.attributes[j].value.Contains("Puff") || metadata.attributes[j].value.Contains("Utility"))
                        {
                            isHoodie = true;
                        }
                    }
                    else if (metadata.attributes[j].trait_type.Contains("Jewellery"))
                    {
                        hasJewellery = true;
                    }
                }

                if (isHoodie && hasJewellery)
                {
                    int upperIndex = 0;
                    int jewelleryIndex = 0;

                    foreach(string layer in layers)
                    {
                        if (layer.Contains("Upper"))
                            upperIndex = layers.IndexOf(layer);

                        if (layer.Contains("Jewellery"))
                            jewelleryIndex = layers.IndexOf(layer);
                    }

                    string temp = layers[upperIndex];
                    layers[upperIndex] = layers[jewelleryIndex];
                    layers[jewelleryIndex] = temp;
                }
            }
        }

        #region SELECTORS
        private void SelectBackground(ref Metadata metadata, ref List<string> layers)
        {

            string attributeType = "Background";
            string attributeName;

            attributeName = m_AllBackgrounds[0][randGenerator.Next(0, m_AllBackgrounds[0].Length)];

            metadata.attributes.Add(new Metadata.Attribute(attributeType, Path.GetFileNameWithoutExtension(attributeName)));
            layers.Add(attributeName);
        }
        private void SelectBodyType(ref Metadata metadata, ref List<string> layers)
        {
            int roll = randGenerator.Next(0, 99);

            string attributeType = "Body Type";
            string attributeName;


            if (roll == 0)
            {
                attributeName = m_AllBodies[3][randGenerator.Next(0, m_AllBodies[3].Length)];
            }
            else if (roll >= 1 && roll <= 5)
            {
                attributeName = m_AllBodies[2][randGenerator.Next(0, m_AllBodies[2].Length)];
            }
            else if (roll >= 6 && roll <= 29)
            {
                attributeName = m_AllBodies[1][randGenerator.Next(0, m_AllBodies[1].Length)];
            }
            else
            {
                attributeName = m_AllBodies[0][randGenerator.Next(0, m_AllBodies[0].Length)];
            }

            metadata.attributes.Add(new Metadata.Attribute(attributeType, Path.GetFileNameWithoutExtension(attributeName)));
            layers.Add(attributeName);
        }
        private void SelectFace(ref Metadata metadata, ref List<string> layers)
        {
            int roll = randGenerator.Next(0, 99);

            string attributeType = "Face";
            string attributeName;

            if (roll == 0)
            {
                attributeName = m_AllFaces[3][randGenerator.Next(0, m_AllFaces[3].Length)];
            }
            else if (roll >= 1 && roll <= 5)
            {
                attributeName = m_AllFaces[2][randGenerator.Next(0, m_AllFaces[2].Length)];
            }
            else if (roll >= 6 && roll <= 29)
            {
                attributeName = m_AllFaces[1][randGenerator.Next(0, m_AllFaces[1].Length)];
            }
            else
            {
                attributeName = m_AllFaces[0][randGenerator.Next(0, m_AllFaces[0].Length)];
            }

            metadata.attributes.Add(new Metadata.Attribute(attributeType, Path.GetFileNameWithoutExtension(attributeName)));
            layers.Add(attributeName);
        }
        private void SelectRudeboyBackground(ref Metadata metadata, ref List<string> layers)
        {
            string attributeType = "Rudeboy Background";
            string attributeName;

            attributeName = m_AllRudeboyBackground[0][randGenerator.Next(0, m_AllRudeboyBackground[0].Length)];

            metadata.attributes.Add(new Metadata.Attribute(attributeType, Path.GetFileNameWithoutExtension(attributeName)));
            layers.Add(attributeName);
        }        
        private void SelectRudeboyEyes(ref Metadata metadata, ref List<string> layers)
        {
            string attributeType = "Rudeboy Eyes";
            string attributeName;

            attributeName = m_AllRudeboyEyes[0][randGenerator.Next(0, m_AllRudeboyEyes[0].Length)];

            metadata.attributes.Add(new Metadata.Attribute(attributeType, Path.GetFileNameWithoutExtension(attributeName)));
            layers.Add(attributeName);
        }        
        private void SelectRudeboyFace(ref Metadata metadata, ref List<string> layers)
        {
            string attributeType = "Rudeboy Face";
            string attributeName;

            attributeName = m_AllRudeboyFace[0][randGenerator.Next(0, m_AllRudeboyFace[0].Length)];

            metadata.attributes.Add(new Metadata.Attribute(attributeType, Path.GetFileNameWithoutExtension(attributeName)));
            layers.Add(attributeName);
        }
        private void SelectRudeboyBadge(ref Metadata metadata, ref List<string> layers)
        {
            string attributeType = "Rudeboy Badge";
            string attributeName;

            attributeName = m_AllRudeboyBadge[0][randGenerator.Next(0, m_AllRudeboyBadge[0].Length)];

            metadata.attributes.Add(new Metadata.Attribute(attributeType, Path.GetFileNameWithoutExtension(attributeName)));
            layers.Add(attributeName);
        }
        private void SelectRudeboyHead(ref Metadata metadata, ref List<string> layers)
        {
            string attributeType = "Rudeboy Head";
            string attributeName;

            attributeName = m_AllRudeboyHead[0][randGenerator.Next(0, m_AllRudeboyHead[0].Length)];

            metadata.attributes.Add(new Metadata.Attribute(attributeType, Path.GetFileNameWithoutExtension(attributeName)));
            layers.Add(attributeName);
        }        private void SelectRudeboyMouth(ref Metadata metadata, ref List<string> layers)
        {
            string attributeType = "Rudeboy Mouth";
            string attributeName;

            attributeName = m_AllRudeboyMouth[0][randGenerator.Next(0, m_AllRudeboyMouth[0].Length)];

            metadata.attributes.Add(new Metadata.Attribute(attributeType, Path.GetFileNameWithoutExtension(attributeName)));
            layers.Add(attributeName);
        }
        private void SelectTSP(ref Metadata metadata, ref List<string> layers)
        {
            int roll = randGenerator.Next(0, 99);

            string attributeType = "TSP";
            string attributeName;

            if (roll == 0)
            {
                attributeName =  m_AllTSP[3][randGenerator.Next(0, m_AllTSP[3].Length)];
            }
            else if (roll >= 1 && roll <= 5)
            {
                attributeName = m_AllTSP[2][randGenerator.Next(0, m_AllTSP[2].Length)];
            }
            else if (roll >= 6 && roll <= 29)
            {
                attributeName = m_AllTSP[1][randGenerator.Next(0, m_AllTSP[1].Length)];
            }
            else
            {
                attributeName = m_AllTSP[0][randGenerator.Next(0, m_AllTSP[0].Length)];
            }

            if(attributeName.Contains("("))
            {
                string colour = attributeName.Split('(')[1].Split(')')[0].Split(' ')[0];

                while (HasAttributeValueSubstring(metadata, colour))
                {
                    if (roll == 0)
                    {
                        attributeName = m_AllTSP[3][randGenerator.Next(0, m_AllTSP[3].Length)];
                    }
                    else if (roll >= 1 && roll <= 5)
                    {
                        attributeName = m_AllTSP[2][randGenerator.Next(0, m_AllTSP[2].Length)];
                    }
                    else if (roll >= 6 && roll <= 29)
                    {
                        attributeName = m_AllTSP[1][randGenerator.Next(0, m_AllTSP[1].Length)];
                    }
                    else
                    {
                        attributeName = m_AllTSP[0][randGenerator.Next(0, m_AllTSP[0].Length)];
                    }
                    colour = attributeName.Split('(')[1].Split(')')[0];
                }
            }
            
            metadata.attributes.Add(new Metadata.Attribute(attributeType, Path.GetFileNameWithoutExtension(attributeName)));
            layers.Add(attributeName);
        }
        private void SelectJewellery(ref Metadata metadata, ref List<string> layers)
        {
            int roll = randGenerator.Next(0, 99);

            if (roll < 25)
            {
                string attributeType = "Jewellery";
                string attributeName;
                int choice = randGenerator.Next(0, 99);
                if(choice <= 39)
                {
                    attributeName = m_AllJewellery[0][randGenerator.Next(0, m_AllJewellery[0].Length)];
                }
                else if (choice >= 40 && choice <= 67)
                {
                    attributeName = m_AllJewellery[1][randGenerator.Next(0, m_AllJewellery[1].Length)];
                }
                else if (choice >= 68 && choice <= 83)
                {
                    attributeName = m_AllJewellery[2][randGenerator.Next(0, m_AllJewellery[2].Length)];
                }
                else
                {
                    attributeName = m_AllJewellery[3][randGenerator.Next(0, m_AllJewellery[3].Length)];
                }

                metadata.attributes.Add(new Metadata.Attribute(attributeType, Path.GetFileNameWithoutExtension(attributeName)));
                layers.Add(attributeName);
            }
        }

        private void SelectUpper(ref Metadata metadata, ref List<string> layers)
        {
            int roll = randGenerator.Next(0, 99);


            //if (roll >= 10)
            {
                string attributeType = "Upper";
                string attributeName;
                int choice = randGenerator.Next(0, 101);
                if (choice <= 29)
                {
                    attributeName = m_AllUpper[0][randGenerator.Next(0, m_AllUpper[0].Length)];
                }
                else if (choice >= 30 && choice <= 51)
                {
                    attributeName = m_AllUpper[1][randGenerator.Next(0, m_AllUpper[1].Length)]; ;
                }
                else if (choice >= 52 && choice <= 71)
                {
                    attributeName = m_AllUpper[2][randGenerator.Next(0, m_AllUpper[2].Length)]; ;
                }
                else if (choice >= 72 && choice <= 84)
                {
                    attributeName = m_AllUpper[3][randGenerator.Next(0, m_AllUpper[3].Length)]; ;
                }
                else if (choice >= 85 && choice <= 90)
                {
                    attributeName = m_AllUpper[4][randGenerator.Next(0, m_AllUpper[4].Length)]; ;
                }
                else if (choice >= 91 && choice <= 96)
                {
                    attributeName = m_AllUpper[5][randGenerator.Next(0, m_AllUpper[5].Length)]; ;
                }
                else
                {
                    attributeName = m_AllUpper[6][randGenerator.Next(0, m_AllUpper[6].Length)]; ;
                }

                metadata.attributes.Add(new Metadata.Attribute(attributeType, Path.GetFileNameWithoutExtension(attributeName)));
                layers.Add(attributeName);
            }
        }

        private void SelectLower(ref Metadata metadata, ref List<string> layers)
        {
            int roll = randGenerator.Next(0, 99);


            if (roll >= 30)
            {
                string attributeType = "Lower Body";
                string attributeName;
                int choice = randGenerator.Next(0, 99);
                if (choice <= 39)
                {
                    attributeName = m_AllLower[0][randGenerator.Next(0, m_AllLower[0].Length)];
                }
                else 
                {
                    attributeName = m_AllLower[1][randGenerator.Next(0, m_AllLower[1].Length)];
                }

                metadata.attributes.Add(new Metadata.Attribute(attributeType, Path.GetFileNameWithoutExtension(attributeName)));
                layers.Add(attributeName);
            }
        }

        private void SelectEarrings(ref Metadata metadata, ref List<string> layers)
        {
            int roll = randGenerator.Next(0, 99);

            if (roll <= 29)
            {
                string attributeType = "Earrings";
                string attributeName;
                int choice = randGenerator.Next(0, 99);
                if (choice <= 49)
                {
                    attributeName = m_AllEarrings[0][randGenerator.Next(0, m_AllEarrings[0].Length)];
                }
                else if(choice >= 50 && choice <= 74)
                {
                    attributeName = m_AllEarrings[1][randGenerator.Next(0, m_AllEarrings[1].Length)];
                }
                else
                {
                    attributeName = m_AllEarrings[2][randGenerator.Next(0, m_AllEarrings[2].Length)];
                }

                metadata.attributes.Add(new Metadata.Attribute(attributeType, Path.GetFileNameWithoutExtension(attributeName)));
                layers.Add(attributeName);
            }
        }

        private void SelectAccessories(ref Metadata metadata, ref List<string> layers)
        {
            int roll = randGenerator.Next(0, 99);

            if (roll <= 19)
            {
                string attributeType = "Accessories";
                string attributeName;
                int choice = randGenerator.Next(0, 99);
                if (choice <= 29)
                {
                    attributeName = m_AllAccessories[0][randGenerator.Next(0, m_AllAccessories[0].Length)];
                }
                else if (choice >= 30 && choice <= 59)
                {
                    attributeName = m_AllAccessories[1][randGenerator.Next(0, m_AllAccessories[1].Length)];
                }
                else if (choice >= 60 && choice <= 79)
                {
                    attributeName = m_AllAccessories[2][randGenerator.Next(0, m_AllAccessories[2].Length)];
                }
                else if (choice >= 80 && choice <= 89)
                {
                    attributeName = m_AllAccessories[3][randGenerator.Next(0, m_AllAccessories[3].Length)];
                }
                else
                {
                    attributeName = m_AllAccessories[4][randGenerator.Next(0, m_AllAccessories[4].Length)];
                }

                metadata.attributes.Add(new Metadata.Attribute(attributeType, Path.GetFileNameWithoutExtension(attributeName)));
                layers.Add(attributeName);
            }
        }

        private void SelectEyewear(ref Metadata metadata, ref List<string> layers, bool hasEarrings)
        {
            int roll = randGenerator.Next(0, 99);

            if (roll <= 29)
            {
                string attributeType = "Eyewear";
                string attributeName;
                int choice = randGenerator.Next(0, 99);
                if (choice <= 20)
                {
                    attributeName = m_AllEyewear[0][randGenerator.Next(0, m_AllEyewear[0].Length)];
                }
                else if (choice >= 21 && choice <= 41)
                {
                    attributeName = m_AllEyewear[1][randGenerator.Next(0, m_AllEyewear[1].Length)];
                }
                else if (choice >= 42 && choice <= 58)
                {
                    attributeName = m_AllEyewear[2][randGenerator.Next(0, m_AllEyewear[2].Length)];
                }
                else if (choice >= 59 && choice <= 74)
                {
                    attributeName = m_AllEyewear[3][randGenerator.Next(0, m_AllEyewear[3].Length)];
                }
                else if (choice >= 75 && choice <= 92)
                {
                    attributeName = m_AllEyewear[4][randGenerator.Next(0, m_AllEyewear[4].Length)];
                }
                else if (choice >= 93 && choice <= 97)
                {
                    attributeName = m_AllEyewear[5][randGenerator.Next(0, m_AllEyewear[5].Length)];
                }
                else
                {
                    attributeName = m_AllEyewear[6][randGenerator.Next(0, m_AllEyewear[6].Length)];
                }
                    
                if(hasEarrings)
                {
                    while (attributeName.Contains("Ski") || attributeName.Contains("Lens"))
                    {
                        choice = randGenerator.Next(0, 99);
                        if (choice <= 20)
                        {
                            attributeName = m_AllEyewear[0][randGenerator.Next(0, m_AllEyewear[0].Length)];
                        }                                                
                        else if (choice >= 21 && choice <= 41)
                        {
                            attributeName = m_AllEyewear[1][randGenerator.Next(0, m_AllEyewear[1].Length)];
                        }
                        else if (choice >= 42 && choice <= 58)
                        {
                            attributeName = m_AllEyewear[2][randGenerator.Next(0, m_AllEyewear[2].Length)];
                        }
                        else if (choice >= 59 && choice <= 74)
                        {
                            attributeName = m_AllEyewear[3][randGenerator.Next(0, m_AllEyewear[3].Length)];
                        }
                        else if (choice >= 75 && choice <= 92)
                        {
                            attributeName = m_AllEyewear[4][randGenerator.Next(0, m_AllEyewear[4].Length)];
                        }
                        else if (choice >= 93 && choice <= 97)
                        {
                            attributeName = m_AllEyewear[5][randGenerator.Next(0, m_AllEyewear[5].Length)];
                        }
                        else
                        {
                            attributeName = m_AllEyewear[6][randGenerator.Next(0, m_AllEyewear[6].Length)];
                        }
                    }
                }
                
                metadata.attributes.Add(new Metadata.Attribute(attributeType, Path.GetFileNameWithoutExtension(attributeName)));
                layers.Add(attributeName);
            }
        }
        private void SelectFootwear(ref Metadata metadata, ref List<string> layers)
        {
            int roll = randGenerator.Next(0, 99);

            if (roll >= 29)
            {
                string attributeType = "FootWear";
                string attributeName;

                attributeName = m_AllFootwear[0][randGenerator.Next(0, m_AllFootwear[0].Length)];

                metadata.attributes.Add(new Metadata.Attribute(attributeType, Path.GetFileNameWithoutExtension(attributeName)));
                layers.Add(attributeName);
            }
        }
        private void SelectHeadwear(ref Metadata metadata, ref List<string> layers, bool hasGlasses)
        {
            int roll = randGenerator.Next(0, 99);


            if (roll >= 29)
            {
                string attributeType = "HeadWear";
                string attributeName;

                attributeName = m_AllHeadwear[0][randGenerator.Next(0, m_AllHeadwear[0].Length)];

                if (hasGlasses)
                {
                    while(attributeName.Contains("Baseball"))
                    {
                        attributeName = m_AllHeadwear[0][randGenerator.Next(0, m_AllHeadwear[0].Length)];
                    }
                }

                metadata.attributes.Add(new Metadata.Attribute(attributeType, Path.GetFileNameWithoutExtension(attributeName)));
                layers.Add(attributeName);
            }
        }
        #endregion
    }
}
// Provides a task scheduler that ensures a maximum concurrency level while
// running on top of the thread pool.
public class LimitedConcurrencyLevelTaskScheduler : TaskScheduler
{
    // Indicates whether the current thread is processing work items.
    [ThreadStatic]
    private static bool _currentThreadIsProcessingItems;

    // The list of tasks to be executed
    private readonly LinkedList<Task> _tasks = new LinkedList<Task>(); // protected by lock(_tasks)

    // The maximum concurrency level allowed by this scheduler.
    private readonly int _maxDegreeOfParallelism;

    // Indicates whether the scheduler is currently processing work items.
    private int _delegatesQueuedOrRunning = 0;

    // Creates a new instance with the specified degree of parallelism.
    public LimitedConcurrencyLevelTaskScheduler(int maxDegreeOfParallelism)
    {
        if (maxDegreeOfParallelism < 1) throw new ArgumentOutOfRangeException("maxDegreeOfParallelism");
        _maxDegreeOfParallelism = maxDegreeOfParallelism;
    }

    // Queues a task to the scheduler.
    protected sealed override void QueueTask(Task task)
    {
        // Add the task to the list of tasks to be processed.  If there aren't enough
        // delegates currently queued or running to process tasks, schedule another.
        lock (_tasks)
        {
            _tasks.AddLast(task);
            if (_delegatesQueuedOrRunning < _maxDegreeOfParallelism)
            {
                ++_delegatesQueuedOrRunning;
                NotifyThreadPoolOfPendingWork();
            }
        }
    }

    // Inform the ThreadPool that there's work to be executed for this scheduler.
    private void NotifyThreadPoolOfPendingWork()
    {
        ThreadPool.UnsafeQueueUserWorkItem(_ =>
        {
            // Note that the current thread is now processing work items.
            // This is necessary to enable inlining of tasks into this thread.
            _currentThreadIsProcessingItems = true;
            try
            {
                // Process all available items in the queue.
                while (true)
                {
                    Task item;
                    lock (_tasks)
                    {
                        // When there are no more items to be processed,
                        // note that we're done processing, and get out.
                        if (_tasks.Count == 0)
                        {
                            --_delegatesQueuedOrRunning;
                            break;
                        }

                        // Get the next item from the queue
                        item = _tasks.First.Value;
                        _tasks.RemoveFirst();
                    }

                    // Execute the task we pulled out of the queue
                    base.TryExecuteTask(item);
                }
            }
            // We're done processing items on the current thread
            finally { _currentThreadIsProcessingItems = false; }
        }, null);
    }

    // Attempts to execute the specified task on the current thread.
    protected sealed override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        // If this thread isn't already processing a task, we don't support inlining
        if (!_currentThreadIsProcessingItems) return false;

        // If the task was previously queued, remove it from the queue
        if (taskWasPreviouslyQueued)
            // Try to run the task.
            if (TryDequeue(task))
                return base.TryExecuteTask(task);
            else
                return false;
        else
            return base.TryExecuteTask(task);
    }

    // Attempt to remove a previously scheduled task from the scheduler.
    protected sealed override bool TryDequeue(Task task)
    {
        lock (_tasks) return _tasks.Remove(task);
    }

    // Gets the maximum concurrency level supported by this scheduler.
    public sealed override int MaximumConcurrencyLevel { get { return _maxDegreeOfParallelism; } }

    // Gets an enumerable of the tasks currently scheduled on this scheduler.
    protected sealed override IEnumerable<Task> GetScheduledTasks()
    {
        bool lockTaken = false;
        try
        {
            Monitor.TryEnter(_tasks, ref lockTaken);
            if (lockTaken) return _tasks;
            else throw new NotSupportedException();
        }
        finally
        {
            if (lockTaken) Monitor.Exit(_tasks);
        }
    }
}