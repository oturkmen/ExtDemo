using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Promoto
{
    public partial class Demo2 : System.Web.UI.Page
    {
        public class ModelItem
        {
            public int protokolay { get; set; }
            public int id { get; set; }
            public double ortalama { get; set; }
            public double netkazanc { get; set; }
            public decimal karoran { get; set; }
            public int adet { get; set; }
            public int adettoplam { get; set; }
            public decimal yuzde { get; set; }
            public decimal kbortalama { get; set; }

            public decimal StrPlanArtis { get; set; }
            public int PersonelAdet { get; set; }
            public decimal PersAdYuzde { get; set; }
            public decimal OngorulenKBOrt { get; set; }

            public class ModelItemDetay
            {
                public int Ay { get; set; }
                public decimal PersonelAdetYuzde { get; set; }
                public decimal USOKisiSayi { get; set; }
                public decimal StratejikKBOrtalama { get; set; }
                public decimal StratejikToplamTutar { get; set; }
                public decimal OngorulenKBOrtalama { get; set; }
                public decimal OngorulenKBBakiye { get; set; }
                public decimal StratejikKarOran { get; set; }
                public decimal StratejikNetKazanc { get; set; }
                public decimal BeklentiKarOran { get; set; }
                public decimal BeklentiNetKazanc { get; set; }
            }

            List<ModelItemDetay> ModelItemDetayList;

            public void detayYap()
            {
                ModelItemDetayList = new List<ModelItemDetay>();
                ModelItemDetay[] ModelItemDetayArray = new ModelItemDetay[protokolay];
                int ProtokolAy = this.protokolay;
                int Adet = this.adet;
                int AdetToplam = this.adettoplam;
                decimal StratejikPlanArtisOran = this.StrPlanArtis;
                int PersonelAdet = this.PersonelAdet;
                decimal PersonelAdetOran = this.PersAdYuzde;
                decimal OngorulenKBOrtalamaRisk = this.OngorulenKBOrt;
                decimal KarOran = this.karoran;

                decimal Ortalama = (decimal)ortalama;
                decimal AdetOran = this.yuzde;
                decimal KisiBasiOrtalama = this.kbortalama;

                StratejikPlanArtisOran = StratejikPlanArtisOran / 100;
                PersonelAdetOran = PersonelAdetOran / 100;
                decimal OranFark = (PersonelAdetOran - AdetOran) / ProtokolAy;
                int MonthModifier = 12;
                int MaxAyByModifier = Int32.Parse(Math.Ceiling((decimal)ProtokolAy / MonthModifier).ToString());
                int MaxAy = MaxAyByModifier * MonthModifier;

                decimal[] StratejikKBOrtalamaMax = new decimal[MaxAy];

                for (int i = 1; i <= MaxAyByModifier; i++)
                {
                    int ArrayPosition = i * MonthModifier - 1;
                    if (i == 1)
                    {
                        StratejikKBOrtalamaMax[i * MonthModifier - 1] = KisiBasiOrtalama + (KisiBasiOrtalama * StratejikPlanArtisOran);
                    }
                    else
                    {
                        Decimal previousOrtalama = StratejikKBOrtalamaMax[((i - 1) * MonthModifier) - 1];
                        StratejikKBOrtalamaMax[i * MonthModifier - 1] = previousOrtalama + (previousOrtalama * StratejikPlanArtisOran);
                    }
                }

                for (int i = 0; i < ProtokolAy; i++)
                {
                    int ModCeilingValue = Int32.Parse(Math.Ceiling((decimal)(i + 1) / MonthModifier).ToString());
                    int ModFloorValue = Int32.Parse(Math.Floor((decimal)(i + 1) / MonthModifier).ToString());
                    if ((i + 1) % MonthModifier == 0)
                    {
                        ModelItemDetayArray[i].StratejikKBOrtalama = StratejikKBOrtalamaMax[i];
                    }
                    else
                    {
                        decimal First;
                        decimal SecondTemp;
                        if (i == 0)
                        {
                            First = KisiBasiOrtalama;
                        }
                        else
                        {
                            First = ModelItemDetayArray[i - 1].StratejikKBOrtalama;
                        }
                        if (ModCeilingValue == 1)
                        {
                            SecondTemp = KisiBasiOrtalama;
                        }
                        else
                        {
                            SecondTemp = StratejikKBOrtalamaMax[ModFloorValue * MonthModifier - 1];
                        }
                        decimal Second = (StratejikKBOrtalamaMax[ModCeilingValue * MonthModifier - 1] - SecondTemp) / MonthModifier;
                        ModelItemDetayArray[i].StratejikKBOrtalama = First + Second;
                    }
                    if (i == ProtokolAy - 1)
                    {
                        ModelItemDetayArray[i].PersonelAdetYuzde = PersonelAdetOran;
                    }
                    else
                    {
                        decimal First;
                        if (i == 0)
                        {
                            First = AdetOran;
                        }
                        else
                        {
                            First = ModelItemDetayArray[i - 1].PersonelAdetYuzde;
                        }
                        ModelItemDetayArray[i].PersonelAdetYuzde = First + OranFark;
                    }

                    ModelItemDetayArray[i].USOKisiSayi = ModelItemDetayArray[i].PersonelAdetYuzde * PersonelAdet;
                    ModelItemDetayArray[i].OngorulenKBOrtalama = OngorulenKBOrtalamaRisk;
                    ModelItemDetayArray[i].OngorulenKBBakiye = ModelItemDetayArray[i].USOKisiSayi * ModelItemDetayArray[i].OngorulenKBOrtalama;
                    ModelItemDetayArray[i].StratejikToplamTutar = ModelItemDetayArray[i].USOKisiSayi * ModelItemDetayArray[i].StratejikKBOrtalama;
                    ModelItemDetayArray[i].StratejikNetKazanc = ModelItemDetayArray[i].StratejikToplamTutar * KarOran;
                    ModelItemDetayArray[i].BeklentiNetKazanc = ModelItemDetayArray[i].OngorulenKBBakiye * KarOran;
                    
                    
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            List<ModelItem> ModelItemList = new List<ModelItem>();
            ModelItemList.Add(
                new ModelItem
                {
                    id = 0,
                    ortalama = 3873432.0,
                    netkazanc = 4786606.0,
                    adet = 591,
                    adettoplam = 1487,
                    StrPlanArtis = 12,
                    PersonelAdet = 10000,
                    PersAdYuzde = 100,
                    OngorulenKBOrt = 2500
                }
                );
            ModelItemList.Add(
                new ModelItem
                {
                    id = 1,
                    ortalama = 91334432,
                    netkazanc = 14124124,
                    adet = 134,
                    adettoplam = 987,
                    StrPlanArtis = 11,
                    PersonelAdet = 8000,
                    PersAdYuzde = 90,
                    OngorulenKBOrt = 3200
                }
                );
            store2.Data = ModelItemList;
            store2.DataBind();
        }
    }
}