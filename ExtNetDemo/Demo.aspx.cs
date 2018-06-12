using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Promoto
{
    public partial class Demo : System.Web.UI.Page
    {            
        public class ModelItem
        {
            public int protokolay { get; set; }
            public int id { get; set; }
            public double Ortalama { get; set; }
            public double NetKazanc { get; set; }
            public decimal KarOran { get; set; }
            public int Adet { get; set; }
            public int AdetToplam { get; set; }
            public decimal Yuzde { get; set; }
            public decimal KBOrtalama { get; set; }

            public decimal StrPlanArtis { get; set; }
            public int PersonelAdet { get; set; }
            public decimal PersAdYuzde { get; set; }
            public decimal OngorulenKBOrt { get; set; }


            public void dataDoldur()
            {
                if (this.Ortalama == 0)
                {
                    this.KarOran = 0;
                }
                else
                {
                    this.KarOran = (decimal)this.NetKazanc / (decimal)this.Ortalama;
                }
                if (AdetToplam == 0)
                {
                    this.Yuzde = 0;
                }
                else
                {
                    this.Yuzde = (decimal)this.Adet / (decimal)this.AdetToplam;
                }
                if (this.Adet == 0)
                {
                    this.KBOrtalama = 0;
                }
                else
                {
                    this.KBOrtalama = (decimal)this.Ortalama / this.Adet;
                }
            }

        }
        public class ModelItemDetay
        {
            public int id { get; set; }
            public double Ortalama { get; set; }
            public double NetKazanc { get; set; }
            public decimal KarOran { get; set; }
            public int Adet { get; set; }
            public int AdetToplam { get; set; }
            public decimal Yuzde { get; set; }
            public decimal KBOrtalama { get; set; }

            public decimal StrPlanArtis { get; set; }
            public int PersonelAdet { get; set; }
            public decimal PersAdYuzde { get; set; }
            public decimal OngorulenKBOrt { get; set; }

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

            public List<ModelItemDetay> DataDoldurDetay(ModelItem mdl)
            {
                ModelItemDetay[] ModelItemDetayArray = new ModelItemDetay[mdl.protokolay];
                for (int y = 0; y < ModelItemDetayArray.Length; y++)
                {
                    ModelItemDetayArray[y] = new ModelItemDetay();
                }
                int ProtokolAy = mdl.protokolay;
                int Adet = mdl.Adet;
                int AdetToplam = mdl.AdetToplam;
                decimal StratejikPlanArtisOran = mdl.StrPlanArtis;
                int PersonelAdet = mdl.PersonelAdet;
                decimal PersonelAdetOran = mdl.PersAdYuzde;
                decimal OngorulenKBOrtalamaRisk = mdl.OngorulenKBOrt;

                decimal Ortalama = (decimal)mdl.Ortalama;
                decimal KarOran = mdl.KarOran;
                decimal AdetOran = mdl.Yuzde;
                decimal KisiBasiOrtalama = mdl.KBOrtalama;

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

                    ModelItemDetayArray[i].Ay = i + 1;
                    ModelItemDetayArray[i].id = i;
                    ModelItemDetayArray[i].Ortalama = mdl.Ortalama;
                    ModelItemDetayArray[i].NetKazanc = mdl.NetKazanc;
                    ModelItemDetayArray[i].KarOran = mdl.KarOran;
                    ModelItemDetayArray[i].Adet = mdl.Adet;
                    ModelItemDetayArray[i].AdetToplam = mdl.AdetToplam;
                    ModelItemDetayArray[i].Yuzde = mdl.Yuzde;
                    ModelItemDetayArray[i].KBOrtalama = mdl.KBOrtalama;
                    ModelItemDetayArray[i].StrPlanArtis = mdl.StrPlanArtis;
                    ModelItemDetayArray[i].PersonelAdet = mdl.PersonelAdet;
                    ModelItemDetayArray[i].PersAdYuzde = mdl.PersAdYuzde;
                    ModelItemDetayArray[i].OngorulenKBOrt = mdl.OngorulenKBOrt;

                    ModelItemDetayArray[i].USOKisiSayi = ModelItemDetayArray[i].PersonelAdetYuzde * PersonelAdet;
                    ModelItemDetayArray[i].OngorulenKBOrtalama = OngorulenKBOrtalamaRisk;
                    ModelItemDetayArray[i].OngorulenKBBakiye = ModelItemDetayArray[i].USOKisiSayi * ModelItemDetayArray[i].OngorulenKBOrtalama;
                    ModelItemDetayArray[i].StratejikToplamTutar = ModelItemDetayArray[i].USOKisiSayi * ModelItemDetayArray[i].StratejikKBOrtalama;
                    ModelItemDetayArray[i].StratejikNetKazanc = ModelItemDetayArray[i].StratejikToplamTutar * KarOran;
                    ModelItemDetayArray[i].BeklentiNetKazanc = ModelItemDetayArray[i].OngorulenKBBakiye * KarOran;
                    ModelItemDetayArray[i].StratejikKarOran = mdl.KarOran;
                    ModelItemDetayArray[i].BeklentiKarOran = mdl.KarOran;

                }
                return ModelItemDetayArray.ToList();
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ModelItem mdl = new ModelItem
            {
                id = 0,
                Ortalama = 3873432.0,
                NetKazanc = 4786606.0,
                Adet = 591,
                AdetToplam = 1487,
                StrPlanArtis = 12,
                PersonelAdet = 10000,
                PersAdYuzde = 100,
                OngorulenKBOrt = 2500,
                protokolay = 36
            };

            mdl.dataDoldur();

            ModelItemDetay mdld = new ModelItemDetay();
            List<ModelItemDetay> ModelItemDetayList = mdld.DataDoldurDetay(mdl);
            store1.Data = mdl;
            store1.DataBind();
            store2.Data = ModelItemDetayList;
            store2.DataBind();
        }
    }
}

//List<ModelItem> ModelItemList = new List<ModelItem>();
//ModelItemList.Add(
//    new ModelItem
//    {
//        id = 0,
//        Ortalama = 3873432.0,
//        NetKazanc = 4786606.0,
//        Adet = 591,
//        AdetToplam = 1487,
//        StrPlanArtis = 12,
//        PersonelAdet = 10000,
//        PersAdYuzde = 100,
//        OngorulenKBOrt = 2500,
//        protokolay= 36
//    }
//    );
//ModelItemList.Add(
//    new ModelItem
//    {
//        id = 1,
//        Ortalama = 91334432,
//        NetKazanc = 14124124,
//        Adet = 134,
//        AdetToplam = 987,
//        StrPlanArtis = 11,
//        PersonelAdet = 8000,
//        PersAdYuzde = 90,
//        OngorulenKBOrt = 3200,
//        protokolay = 36
//    }
//    );