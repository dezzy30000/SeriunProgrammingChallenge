using NUnit.Framework;
using SeriunProgrammingChallenge.Console.Services.Exporters;
using SeriunProgrammingChallenge.Console.Services.Importers;
using System;
using System.IO;
using System.Linq;

namespace SeriunProgrammingChallenge.Tests.ServicesTests
{
    public class ReportsServiceTests
    {
        private const string FileName = "feed.csv";

        [Test]
        public void GivenACustomCSVFeedWhenImportedThenTheCorrectReportsShouldBeGenerated()
        {
            var datalines = new[] 
            {
                "2469,29/05/2016,MS55999,Zig Memory System Wink of Stella Brush Glitter Marker Clear,4.99,1",
                "2469,29/05/2016,MS55000,Zig Memory System Wink of Stella Brush Glitter Marker White,4.99,1",
                "2469,29/05/2016,MS55102,Zig Memory System Wink of Stella Brush Glitter Marker Silver,4.99,1",
                "2469,29/05/2016,MS55101,Zig Memory System Wink of Stella Brush Glitter Marker Gold,4.99,100",
                "2470,29/05/2016,GROWO401311,Numbers Inset & Ovals & Tags Groovi Baby Plate,7.49,1",
                "2470,29/05/2016,GROBI4014501,Groovi Baby Plate A6 Large Bird & Branch,4.79,1",
                "2471,29/05/2016,COLLCEM12,Collall Photoglue,2.99,4",
                "2472,29/05/2016,GROPA40014,Groovi Plate Set Border Plates 1 & 2 ,14.99,1",
                "2472,29/05/2016,GROCH40015,Groovi Plate Border Set Bunting Wave-Alphabet & Christmas,14.99,1",
                "2473,29/05/2016,ACETHR001,Clear Heat Resistant Acetate,2.79,1",
                "2473,29/05/2016,SH131WH,3 Feather Spray - White,0.19,3",
                "2474,29/05/2016,GROAC40091,Clarity Groovi Tool Bag,1.99,1",
                "2474,29/05/2016,GROWO4027603,Happy Birthday  Groovi Plate A5 Square,7.49,1",
                "2477,30/05/2016,GROCN4028801,Groovi Baby Plate A6 Rocking Horse,4.79,1",
                "2477,30/05/2016,GROPE4028003,Geisha Groovi Plate A5 Square,7.49,1",
                "2477,30/05/2016,GROWO4027403,Best Wishes Groovi Plate A5 Square,7.49,50",
                "2478,30/05/2016,GROFL4021701,Groovi Baby Plate A6 Vases,4.79,1",
                "2478,30/05/2016,GROAC40025,\"Clarity Groovi Parchment Paper 7\"\" x 7\"\"\",4.29,1",
                "2478,30/05/2016,GROWO4010709,Groovi Plate Border Set Line Sentiments ,22.49,1",
                "2479,30/05/2016,GROLA4000703,Groovi Plate A5 Square Mountain Hills ,7.49,1",
                "2479,30/05/2016,GROPA4015609,Groovi Border Plate Half Tone,7.49,1",
                "2479,30/05/2016,GROBI4027701,Groovi Baby Plate A6 Small Bird & Feeder,4.79,1",
                "2480,30/05/2016,CR56329,MDF Wooden Letter T (13cm),1.79,1",
                "2480,30/05/2016,CR56314,MDF Wooden Letter E (13cm),1.79,1",
                "2480,30/05/2016,CR56318,MDF Wooden Letter I (13cm),1.79,1",
                "2480,30/05/2016,CR56321,MDF Wooden Letter L (13cm),1.79,2",
                "2480,30/05/2016,CR56324,MDF Wooden Letter O (13cm),1.79,1",
                "2481,30/05/2016,CEMDFCRACK,MDF Large Christmas Crackers by Creative Expressions,3.49,25",
                "2481,30/05/2016,CEMDFEGG,MDF Egg Stand by Creative Expressions,6.49,1",
                "2482,30/05/2016,SPECNSPACLE3,Spectrum Noir Sparkle Pens Clear Overlay,9.99,1",
                "2483,30/05/2016,AFRFLR001,Craft Consortium Always and Forever The Wedding Collection Resin Flowers - Ivory,2.79,1",
                "2483,30/05/2016,JL126,Brads-Baby Buttons,1.89,1",
                "2483,30/05/2016,CRRHINEROSEGO,Rose Gold Effect Resin Buttons (16mm),1.49,1",
                "2484,31/05/2016,GROFL40088,Groovi Plate Set Blooming Poppies,14.99,1",
                "2484,31/05/2016,GROMA40002,Groovi Plate Alphabet Mate,11.49,5",
                "2485,31/05/2016,SPECNSPACLE3,Spectrum Noir Sparkle Pens Clear Overlay,9.99,10",
                "2486,31/05/2016,GROFL40014,Groovi Plates Set Blooming Peonies/Sprig Pattern,14.99,1",
                "2486,31/05/2016,GROOB4031403,Oriental Fan & Bamboo Groovi Plate A5 Square,14.99,1",
                "2487,31/05/2016,CBC077,MDF Castle Keep Money Box,5.99,2",
                "2487,31/05/2016,BBBIH,Brusho Basics Book by Isobel Hall,5.99,1",
                "2488,01/06/2016,GROAC40091,Clarity Groovi Tool Bag,1.99,1",
                "2488,01/06/2016,GROOVIALBUM,Groovi Album,10.99,1",
                "2490,01/06/2016,GROPA4004609,Groovi Border Plates Set Lace Set 1 & 2,14.99,1",
                "2490,01/06/2016,GROPA4012609,Groovi Border Plates Set Lace Corners,7.49,1",
                "2490,01/06/2016,GROGG4020212,Groovi Piercing Grid Plate - Straight,11.49,1",
                "2490,01/06/2016,GROGG4020112,Groovi Piercing Grid Plate - Diagonal,11.49,1",
                "2490,01/06/2016,GROAC40203,Groovi Piercing Tools 0.95mm,4.79,1",
                "2490,01/06/2016,GROBI4004303,Groovi Plate Set Three Doves & Sprig ,14.99,1",
                "2491,02/06/2016,CEMDFTAG,MDF Tags by Creative Expressions,3.79,2",
                "2493,02/06/2016,GROFL40081,Groovi Plate Set Frilly Circles ,14.99,1"
            };

            var reportsExporter = new ReportsExporter();
            var importer = new CSVImporter(datalines, new[] { reportsExporter });

            if (!importer.Import())
            {
                Assert.Fail("Failed to import and hence test failed.");
            }

            var reports = reportsExporter.GetReportService();
            
            var popularItems = reports.TopMostPopularProducts(5);
            var averageOrderValue = reports.AverageOrderValue();
            var totalValueOfDiscounts = reports.TotalValueOfDiscountsIssuedIfForProduct("MS55101", 20);

            Assert.AreEqual(69.57m, averageOrderValue);
            Assert.AreEqual(1361.07m, totalValueOfDiscounts);

            Assert.AreEqual(5, popularItems.Length);

            Assert.AreEqual("MS55101", popularItems[0].ItemCode);
            Assert.AreEqual(4.99m, popularItems[0].Price);
            Assert.AreEqual("Zig Memory System Wink of Stella Brush Glitter Marker Gold", popularItems[0].Description);

            Assert.AreEqual("GROWO4027403", popularItems[1].ItemCode);
            Assert.AreEqual(7.49m, popularItems[1].Price);
            Assert.AreEqual("Best Wishes Groovi Plate A5 Square", popularItems[1].Description);

            Assert.AreEqual("CEMDFCRACK", popularItems[2].ItemCode);
            Assert.AreEqual(3.49m, popularItems[2].Price);
            Assert.AreEqual("MDF Large Christmas Crackers by Creative Expressions", popularItems[2].Description);

            Assert.AreEqual("SPECNSPACLE3", popularItems[3].ItemCode);
            Assert.AreEqual(9.99m, popularItems[3].Price);
            Assert.AreEqual("Spectrum Noir Sparkle Pens Clear Overlay", popularItems[3].Description);

            Assert.AreEqual("GROMA40002", popularItems[4].ItemCode);
            Assert.AreEqual(11.49m, popularItems[4].Price);
            Assert.AreEqual("Groovi Plate Alphabet Mate", popularItems[4].Description);
        }

        [Test]
        public void GivenACSVFeedWhenImportedThenTheCorrectReportsShouldBeGenerated()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, FileName);

            var reportsExporter = new ReportsExporter();
            var importer = new CSVImporter(File.ReadLines(filePath).Skip(1), new[] { reportsExporter });

            if (!importer.Import())
            {
                Assert.Fail("Failed to import and hence test failed.");
            }

            var reports = reportsExporter.GetReportService();

            var popularItems = reports.TopMostPopularProducts(5);
            var averageOrderValue = reports.AverageOrderValue();
            var totalValueOfDiscounts = reports.TotalValueOfDiscountsIssuedIfForProduct("MS55101", 20);

            Assert.AreEqual(19.09m, averageOrderValue);
            Assert.AreEqual(16126.35, totalValueOfDiscounts);

            Assert.AreEqual(5, popularItems.Length);

            Assert.AreEqual("PTYARN2", popularItems[0].ItemCode);
            Assert.AreEqual(0.15m, popularItems[0].Price);
            Assert.AreEqual("Textile Yarn - Recycled Flat Beige", popularItems[0].Description);

            Assert.AreEqual("GROAC40345", popularItems[1].ItemCode);
            Assert.AreEqual(3.79m, popularItems[1].Price);
            Assert.AreEqual("\"Groovi Guard 7\"\" x 7\"\"\"", popularItems[1].Description);

            Assert.AreEqual("GROAC40025", popularItems[2].ItemCode);
            Assert.AreEqual(4.29m, popularItems[2].Price);
            Assert.AreEqual("\"Clarity Groovi Parchment Paper 7\"\" x 7\"\"\"", popularItems[2].Description);

            Assert.AreEqual("GROTE40436", popularItems[3].ItemCode);
            Assert.AreEqual(14.99m, popularItems[3].Price);
            Assert.AreEqual("Groovi A4 Box Template - Christmas Cracker", popularItems[3].Description);

            Assert.AreEqual("GROFL40397", popularItems[4].ItemCode);
            Assert.AreEqual(13.99m, popularItems[4].Price);
            Assert.AreEqual("Alphabet Picture Frame A4 Groovi Plate by Jayne Nestorenko", popularItems[4].Description);
        }
    }
}
