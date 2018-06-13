using Ext.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

public class Sema
{

    public DataTable SemaCont;
    public string[] CustomTitleArray;
    /*ortak parametreler*/
    public string Title;
    public int ProtokolAy;
    public string Kod;
    public decimal ToplamOrtalama;
    public int Adet;

    //şema 1
    public decimal NetKazanc;
    public int AdetToplam;
    public decimal StratejikPlanArtisOran;
    public int PersonelAdet;
    public decimal PersonelAdetOran;
    public decimal OngorulenKBOrtalamaRisk;

    //şema 2

    public decimal OdenenMaasTutar;
    public decimal YillikMaasArtisOran;

    //şema 4
    public decimal TKGenelKarsilikOran;
    public decimal TCMBPolitikaFaizOran;

    //şema 5
    public decimal OzelKarsiliklarKarsilikOran;
    public decimal OzelKarsiliklarTakipOran;

    //şema 6
    public decimal ToplamOrtalamaBireyselKredi;
    public decimal TakipOnlemePayi;

    //şema 7

    public decimal KurumDigerKazancAylikOrtalama;

    //şema 8

    public int AylikIslemAdet;
    public decimal BirimMaliyet;
    public decimal NBDStrateji;
    public decimal NBDBeklenti;

    //sema10

    public decimal KBAylikOrtalamaRisk;

    /*açılacak pencere ile ilgili ayarlar*/
    public bool ButtonHidden;
    public bool MiddlePanelHidden;
    public string WindowID;

    public DataTable dtContentGridHeader;
    public DataTable dtContentPanel;
    public DataTable dtContentGridFooter;
    public Sema(string DetayKod)
    {
        Kod = DetayKod;
        ButtonHidden = true;
        MiddlePanelHidden = false;
    }
    public void FillDataTable()
    {
        switch (Kod)
        {
            case "BKT":
            case "OKT":
            case "KOT":
            case "KOK":
            case "MAT":
            case "M1H":
            case "M2H":
                WindowID = "WindowDetailSema1";
                ButtonHidden = false;
                MiddlePanelHidden = false;
                Sema1 Sema1 = new Sema1(Title, Kod, ProtokolAy, ToplamOrtalama, NetKazanc, Adet, AdetToplam, StratejikPlanArtisOran, PersonelAdet, PersonelAdetOran, OngorulenKBOrtalamaRisk);
                this.dtContentGridHeader = Sema1.dtContentGridHeader;
                this.dtContentPanel = Sema1.dtContentPanel;
                this.dtContentGridFooter = Sema1.dtContentGridFooter;
                break;
            case "AFH":
                WindowID = "WindowDetailSema2";
                ButtonHidden = false;
                MiddlePanelHidden = false;
                Sema2 Sema2 = new Sema2(Title, Kod, ProtokolAy, OdenenMaasTutar, NetKazanc, Adet, YillikMaasArtisOran, PersonelAdet);
                this.dtContentGridHeader = Sema2.dtContentGridHeader;
                this.dtContentPanel = Sema2.dtContentPanel;
                this.dtContentGridFooter = Sema2.dtContentGridFooter;
                break;
            case "VST":
            case "VSY":
            case "ALY":
            case "VDT":
            case "VDY":
            case "T9H":
            case "BST":
            case "BSO":
            case "SGH":
                WindowID = "WindowDetailSema3";
                ButtonHidden = false;
                MiddlePanelHidden = false;
                Sema3 Sema3 = new Sema3(Title, Kod, ProtokolAy, ToplamOrtalama, NetKazanc, Adet, PersonelAdet, StratejikPlanArtisOran, PersonelAdet, PersonelAdetOran, OngorulenKBOrtalamaRisk);
                this.dtContentGridHeader = Sema3.dtContentGridHeader;
                this.dtContentPanel = Sema3.dtContentPanel;
                this.dtContentGridFooter = Sema3.dtContentGridFooter;
                break;
            case "TKG":
                ButtonHidden = true;
                MiddlePanelHidden = false;
                WindowID = "WindowDetailSema4";
                Sema4 Sema4 = new Sema4(Title, Kod, ProtokolAy, ToplamOrtalamaBireyselKredi, TKGenelKarsilikOran, TCMBPolitikaFaizOran, SemaCont);
                this.dtContentGridHeader = Sema4.dtContentGridHeader;
                this.dtContentPanel = Sema4.dtContentPanel;
                this.dtContentGridFooter = Sema4.dtContentGridFooter;
                break;
            case "OKG":
                WindowID = "WindowDetailSema5";
                Sema5 Sema5 = new Sema5(Title, Kod, ProtokolAy, ToplamOrtalamaBireyselKredi, OzelKarsiliklarKarsilikOran, OzelKarsiliklarTakipOran, TCMBPolitikaFaizOran, SemaCont);
                ButtonHidden = true;
                MiddlePanelHidden = false;
                this.dtContentGridHeader = Sema5.dtContentGridHeader;
                this.dtContentPanel = Sema5.dtContentPanel;
                this.dtContentGridFooter = Sema5.dtContentGridFooter;
                break;
            case "TOP":
                WindowID = "WindowDetailSema6";
                ButtonHidden = false;
                MiddlePanelHidden = false;
                Sema6 Sema6 = new Sema6(Title, Kod, ProtokolAy, ToplamOrtalamaBireyselKredi, TakipOnlemePayi);
                this.dtContentGridHeader = Sema6.dtContentGridHeader;
                this.dtContentPanel = Sema6.dtContentPanel;
                this.dtContentGridFooter = Sema6.dtContentGridFooter;
                break;
            case "TOT":
            case "KDK":
                WindowID = "WindowDetailSema7";
                ButtonHidden = false;
                MiddlePanelHidden = false;
                Sema7 Sema7 = new Sema7(Title, Kod, ProtokolAy, OngorulenKBOrtalamaRisk, PersonelAdet);
                this.dtContentGridHeader = Sema7.dtContentGridHeader;
                this.dtContentPanel = Sema7.dtContentPanel;
                this.dtContentGridFooter = Sema7.dtContentGridFooter;
                break;
            case "EMM":
                WindowID = "WindowDetailSema8";
                ButtonHidden = true;
                MiddlePanelHidden = true;
                Sema8 Sema8 = new Sema8(Title, Kod, ProtokolAy, AylikIslemAdet, BirimMaliyet);
                this.dtContentGridHeader = Sema8.dtContentGridHeader;
                this.dtContentPanel = Sema8.dtContentPanel;
                this.dtContentGridFooter = Sema8.dtContentGridFooter;
                break;
            case "AMM":
            case "USM":
                WindowID = "WindowDetailSema9";
                ButtonHidden = true;
                MiddlePanelHidden = true;
                Sema9 Sema9 = new Sema9(Kod, Title, ProtokolAy, AylikIslemAdet, BirimMaliyet);
                this.dtContentGridHeader = Sema9.dtContentGridHeader;
                this.dtContentPanel = Sema9.dtContentPanel;
                this.dtContentGridFooter = Sema9.dtContentGridFooter;
                CustomTitleArray = Sema9.CustomTitleArray;
                break;
            case "DDH":
            case "DKM":
                WindowID = "WindowDetailSema10";
                ButtonHidden = false;
                MiddlePanelHidden = false;
                Sema10 Sema10 = new Sema10(Title, Kod, ProtokolAy, KBAylikOrtalamaRisk, PersonelAdet);
                this.dtContentGridHeader = Sema10.dtContentGridHeader;
                this.dtContentPanel = Sema10.dtContentPanel;
                this.dtContentGridFooter = Sema10.dtContentGridFooter;
                break;
            default:
                //hata
                break;
        }
    }
}

public class Sema1
{
    public DataTable dtContentGridHeader;
    public DataTable dtContentPanel;
    public DataTable dtContentGridFooter;
    public Sema1(string title, string Kod, int ProtokolAy, decimal Ortalama, decimal NetKazanc, int Adet, int AdetToplam, decimal StratejikPlanArtisOran, int PersonelAdet, decimal PersonelAdetOran, decimal OngorulenKBOrtalamaRisk)
    {
        decimal KarOran;
        decimal AdetOran;
        decimal KisiBasiOrtalama;
        if (Ortalama == 0)
        {
            KarOran = 0;
        }
        else
        {
            KarOran = NetKazanc / Ortalama;
        }
        if (AdetToplam == 0)
        {
            AdetOran = 0;
        }
        else
        {
            AdetOran = (decimal)Adet / (decimal)AdetToplam;
        }
        if (Adet == 0)
        {
            KisiBasiOrtalama = 0;
        }
        else
        {
            KisiBasiOrtalama = Ortalama / Adet;
        }

        dtContentGridHeader = new DataTable();
        dtContentGridHeader.Columns.Add("MevcutDurum", typeof(string));
        dtContentGridHeader.Columns.Add("Kod", typeof(string));
        dtContentGridHeader.Columns.Add("Ortalama", typeof(decimal));
        dtContentGridHeader.Columns.Add("NetKazanc", typeof(decimal));
        dtContentGridHeader.Columns.Add("KarOran", typeof(decimal));
        dtContentGridHeader.Columns.Add("Adet", typeof(int));
        dtContentGridHeader.Columns.Add("AdetToplam", typeof(int));
        dtContentGridHeader.Columns.Add("AdetOran", typeof(int));
        dtContentGridHeader.Columns.Add("KisiBasiOrtalama", typeof(decimal));

        dtContentGridHeader.Rows.Add(title, Kod, Ortalama, NetKazanc, KarOran, Adet, AdetToplam, AdetOran, KisiBasiOrtalama);

        dtContentPanel = new DataTable();
        dtContentPanel.Columns.Add("COL_COMP_ID", typeof(string));
        dtContentPanel.Columns.Add("COL_VAL", typeof(decimal));
        dtContentPanel.Rows.Add("NumberFieldStratejikPlanArtisOran", StratejikPlanArtisOran);
        dtContentPanel.Rows.Add("NumberFieldPersonelAdet", PersonelAdet);
        dtContentPanel.Rows.Add("NumberFieldPersonelAdetOran", PersonelAdetOran);
        dtContentPanel.Rows.Add("NumberFieldOngorulenKBOrtalamaRisk", OngorulenKBOrtalamaRisk);

        dtContentGridFooter = new DataTable();
        dtContentGridFooter.Columns.Add("Ay", typeof(int));
        dtContentGridFooter.Columns.Add("PersonelAdetYuzde", typeof(decimal));
        dtContentGridFooter.Columns.Add("USOKisiSayi", typeof(decimal));
        dtContentGridFooter.Columns.Add("StratejikKBOrtalama", typeof(decimal));
        dtContentGridFooter.Columns.Add("StratejikToplamTutar", typeof(decimal));
        dtContentGridFooter.Columns.Add("OngorulenKBOrtalama", typeof(decimal));
        dtContentGridFooter.Columns.Add("OngorulenKBBakiye", typeof(decimal));
        dtContentGridFooter.Columns.Add("StratejikKarOran", typeof(decimal));
        dtContentGridFooter.Columns.Add("StratejikNetKazanc", typeof(decimal));
        dtContentGridFooter.Columns.Add("BeklentiKarOran", typeof(decimal));
        dtContentGridFooter.Columns.Add("BeklentiNetKazanc", typeof(decimal));

        /////////////// bak
        decimal[] PersAdYuzde = new decimal[ProtokolAy];
        decimal[] USOKisiSayi = new decimal[ProtokolAy];
        decimal[] OngorulenKBOrtalama = new decimal[ProtokolAy];
        decimal[] StratejikKBOrtalama = new decimal[ProtokolAy];
        decimal[] StratejikToplamTutar = new decimal[ProtokolAy];
        decimal[] OngorulenKBBakiye = new decimal[ProtokolAy];
        decimal[] StratejikNetKazanc = new decimal[ProtokolAy];
        decimal[] BeklentiNetKazanc = new decimal[ProtokolAy];


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
                StratejikKBOrtalama[i] = StratejikKBOrtalamaMax[i];
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
                    First = StratejikKBOrtalama[i - 1];
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
                StratejikKBOrtalama[i] = First + Second;
            }
            if (i == ProtokolAy - 1)
            {
                PersAdYuzde[i] = PersonelAdetOran;
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
                    First = PersAdYuzde[i - 1];
                }
                PersAdYuzde[i] = First + OranFark;
            }

            USOKisiSayi[i] = PersAdYuzde[i] * PersonelAdet;
            OngorulenKBOrtalama[i] = OngorulenKBOrtalamaRisk;
            OngorulenKBBakiye[i] = USOKisiSayi[i] * OngorulenKBOrtalama[i];
            StratejikToplamTutar[i] = USOKisiSayi[i] * StratejikKBOrtalama[i];
            StratejikNetKazanc[i] = StratejikToplamTutar[i] * KarOran;
            BeklentiNetKazanc[i] = OngorulenKBBakiye[i] * KarOran;

            dtContentGridFooter.Rows.Add(i + 1, PersAdYuzde[i] * 100, USOKisiSayi[i], StratejikKBOrtalama[i], StratejikToplamTutar[i], OngorulenKBOrtalama[i], OngorulenKBBakiye[i], KarOran, StratejikNetKazanc[i], KarOran, BeklentiNetKazanc[i]);
            
        }
        /////////////////////

        //for (int i = 0; i < ProtokolAy; i++)
        //{
        //    dtContentGridFooter.Rows.Add(i + 1, 0, 0, 0, 0, 0, 0, KarOran, 0, KarOran, 0);

        //}
    }
}

public class Sema2
{
    public DataTable dtContentGridHeader;
    public DataTable dtContentPanel;
    public DataTable dtContentGridFooter;
    public Sema2(string title, string Kod, int ProtokolAy, decimal Ortalama, decimal NetKazanc, int Adet, decimal YillikMaasArtisOran, int PersonelAdet)
    {
        int TalepKisiAdet = PersonelAdet;
        decimal KarOran;
        decimal KisiBasiOrtalama;
        if (Ortalama == 0)
        {
            KarOran = 0;
        }
        else
        {
            KarOran = NetKazanc / Ortalama;
        }
        if (Adet == 0)
        {
            KisiBasiOrtalama = 0;
        }
        else
        {
            KisiBasiOrtalama = Ortalama / Adet;
        }
        decimal TalepKisiAdediKBOrtalama = KisiBasiOrtalama * TalepKisiAdet;

        dtContentGridHeader = new DataTable();
        dtContentGridHeader.Columns.Add("MevcutDurum", typeof(string));
        dtContentGridHeader.Columns.Add("Kod", typeof(string));
        dtContentGridHeader.Columns.Add("Ortalama", typeof(decimal));
        dtContentGridHeader.Columns.Add("NetKazanc", typeof(decimal));
        dtContentGridHeader.Columns.Add("KarOran", typeof(decimal));
        dtContentGridHeader.Columns.Add("Adet", typeof(int));
        dtContentGridHeader.Columns.Add("KisiBasiOrtalama", typeof(decimal));
        dtContentGridHeader.Columns.Add("TalepKisiAdedi", typeof(int));
        dtContentGridHeader.Columns.Add("TalepKisiAdediKBOrtalama", typeof(decimal));
        dtContentGridHeader.Rows.Add(title, Kod, Ortalama, NetKazanc, KarOran, Adet, KisiBasiOrtalama, PersonelAdet, TalepKisiAdediKBOrtalama);

        dtContentPanel = new DataTable();
        dtContentPanel.Columns.Add("COL_COMP_ID", typeof(string));
        dtContentPanel.Columns.Add("COL_VAL", typeof(decimal));
        dtContentPanel.Rows.Add("NumberFieldYillikMaasArtisOran", YillikMaasArtisOran);
        dtContentPanel.Rows.Add("NumberFieldPersonelAdet", PersonelAdet);

        dtContentGridFooter = new DataTable();
        dtContentGridFooter.Columns.Add("Ay", typeof(int));
        dtContentGridFooter.Columns.Add("YillikOngorulenToplamTutar", typeof(decimal));
        dtContentGridFooter.Columns.Add("KarOran", typeof(decimal));
        dtContentGridFooter.Columns.Add("BeklentiNetKazanc", typeof(decimal));

        decimal[] ToplamTutarArray = new decimal[ProtokolAy];
        decimal[] BeklentiNetKazanc = new decimal[ProtokolAy];

        YillikMaasArtisOran = YillikMaasArtisOran / 100;
        int MonthModifier = 12;
        int MaxAyByModifier = Int32.Parse(Math.Ceiling((decimal)ProtokolAy / MonthModifier).ToString());
        int MaxAy = MaxAyByModifier * MonthModifier;

        decimal[] ToplamTutarArrayMax = new decimal[MaxAy];

        for (int i = 1; i <= MaxAyByModifier; i++)
        {
            decimal toplamtutarIslem;
            int ArrayPosition = i * MonthModifier - 1;
            if (i == 1)
            {
                toplamtutarIslem = Ortalama;
            }
            else
            {
                toplamtutarIslem = ToplamTutarArrayMax[((i - 1) * MonthModifier) - 1];
            }
            ToplamTutarArrayMax[i * MonthModifier - 1] = toplamtutarIslem + (toplamtutarIslem * YillikMaasArtisOran);
        }

        for (int i = 0; i < ProtokolAy; i++)
        {

            if ((i + 1) % MonthModifier == 0)
            {
                ToplamTutarArray[i] = ToplamTutarArrayMax[i];
            }
            else
            {
                int ModCeilingValue = Int32.Parse(Math.Ceiling((decimal)(i + 1) / MonthModifier).ToString());
                int ModFloorValue = Int32.Parse(Math.Floor((decimal)(i + 1) / MonthModifier).ToString());
                decimal SecondTemp;
                decimal First;
                if (i == 0)
                {
                    First = TalepKisiAdediKBOrtalama;
                    SecondTemp = TalepKisiAdediKBOrtalama;
                }
                else
                {
                    First = ToplamTutarArray[i - 1];
                    if (ModCeilingValue == 1)
                    {
                        SecondTemp = Ortalama;
                    }
                    else
                    {
                        SecondTemp = ToplamTutarArrayMax[ModFloorValue * MonthModifier - 1];
                    }
                }

                ToplamTutarArray[i] = First + (ToplamTutarArrayMax[ModCeilingValue * MonthModifier - 1] - SecondTemp) / 12;
            }
            BeklentiNetKazanc[i] = ToplamTutarArray[i] * KarOran;
            dtContentGridFooter.Rows.Add(i + 1, ToplamTutarArray[i], KarOran, BeklentiNetKazanc[i]);
        }
    }
}

public class Sema3
{
    public DataTable dtContentGridHeader;
    public DataTable dtContentPanel;
    public DataTable dtContentGridFooter;
    public Sema3(string title, string Kod, int ProtokolAy, decimal Ortalama, decimal NetKazanc, int Adet, int TalepKisiAdedi, decimal StratejikPlanArtisOran, int PersonelAdet, decimal PersonelAdetOran, decimal OngorulenKBOrtalamaRisk)
    {

        decimal KarOran = 0;
        decimal KisiBasiOrtalama;
        if (Ortalama == 0)
        {
            KarOran = 0;
        }
        else
        {
            KarOran = NetKazanc / Ortalama;
        }
        if (Adet == 0)
        {
            KisiBasiOrtalama = 0;
        }
        else
        {
            KisiBasiOrtalama = Ortalama / Adet;
        }
        decimal TalepKisiAdediKBOrtalama = TalepKisiAdedi * KisiBasiOrtalama;

        dtContentGridHeader = new DataTable();
        dtContentGridHeader.Columns.Add("MevcutDurum", typeof(string));
        dtContentGridHeader.Columns.Add("Kod", typeof(string));
        dtContentGridHeader.Columns.Add("Ortalama", typeof(decimal));
        dtContentGridHeader.Columns.Add("NetKazanc", typeof(decimal));
        dtContentGridHeader.Columns.Add("KarOran", typeof(decimal));
        dtContentGridHeader.Columns.Add("Adet", typeof(int));
        dtContentGridHeader.Columns.Add("KisiBasiOrtalama", typeof(decimal));
        dtContentGridHeader.Columns.Add("TalepKisiAdedi", typeof(int));
        dtContentGridHeader.Columns.Add("TalepKisiAdediKBOrtalama", typeof(decimal));

        dtContentGridHeader.Rows.Add(title, Kod, Ortalama, NetKazanc, KarOran, Adet, KisiBasiOrtalama, TalepKisiAdedi, TalepKisiAdediKBOrtalama);

        dtContentPanel = new DataTable();
        dtContentPanel.Columns.Add("COL_COMP_ID", typeof(string));
        dtContentPanel.Columns.Add("COL_VAL", typeof(decimal));
        dtContentPanel.Rows.Add("NumberFieldStratejikPlanArtisOran", StratejikPlanArtisOran);
        dtContentPanel.Rows.Add("NumberFieldPersonelAdet", PersonelAdet);
        dtContentPanel.Rows.Add("NumberFieldPersonelAdetOran", PersonelAdetOran);
        dtContentPanel.Rows.Add("NumberFieldOngorulenKBOrtalamaRisk", OngorulenKBOrtalamaRisk);

        dtContentGridFooter = new DataTable();
        dtContentGridFooter.Columns.Add("Ay", typeof(int));
        dtContentGridFooter.Columns.Add("PersonelAdetYuzde", typeof(decimal));
        dtContentGridFooter.Columns.Add("USOKisiSayi", typeof(decimal));
        dtContentGridFooter.Columns.Add("StratejikKBOrtalama", typeof(decimal));
        dtContentGridFooter.Columns.Add("StratejikToplamTutar", typeof(decimal));
        dtContentGridFooter.Columns.Add("OngorulenKBOrtalama", typeof(decimal));
        dtContentGridFooter.Columns.Add("OngorulenKBBakiye", typeof(decimal));
        dtContentGridFooter.Columns.Add("StratejikKarOran", typeof(decimal));
        dtContentGridFooter.Columns.Add("StratejikNetKazanc", typeof(decimal));
        dtContentGridFooter.Columns.Add("BeklentiKarOran", typeof(decimal));
        dtContentGridFooter.Columns.Add("BeklentiNetKazanc", typeof(decimal));

        /////////////// bak
        decimal[] PersAdYuzde = new decimal[ProtokolAy];
        decimal[] USOKisiSayi = new decimal[ProtokolAy];
        decimal[] OngorulenKBOrtalama = new decimal[ProtokolAy];
        decimal[] StratejikKBOrtalama = new decimal[ProtokolAy];
        decimal[] StratejikToplamTutar = new decimal[ProtokolAy];
        decimal[] OngorulenKBBakiye = new decimal[ProtokolAy];
        decimal[] StratejikNetKazanc = new decimal[ProtokolAy];
        decimal[] BeklentiNetKazanc = new decimal[ProtokolAy];
        StratejikPlanArtisOran = StratejikPlanArtisOran / 100;
        PersonelAdetOran = PersonelAdetOran / 100;

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
                StratejikKBOrtalama[i] = StratejikKBOrtalamaMax[i];
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
                    First = StratejikKBOrtalama[i - 1];
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
                StratejikKBOrtalama[i] = First + Second;
            }
            PersAdYuzde[i] = PersonelAdetOran;
            USOKisiSayi[i] = PersAdYuzde[i] * TalepKisiAdedi;
            StratejikToplamTutar[i] = USOKisiSayi[i] * StratejikKBOrtalama[i];
            OngorulenKBOrtalama[i] = OngorulenKBOrtalamaRisk;
            OngorulenKBBakiye[i] = USOKisiSayi[i] * OngorulenKBOrtalama[i];

            StratejikNetKazanc[i] = StratejikToplamTutar[i] * KarOran;
            BeklentiNetKazanc[i] = OngorulenKBBakiye[i] * KarOran;

            dtContentGridFooter.Rows.Add(i + 1, PersAdYuzde[i] * 100, USOKisiSayi[i], StratejikKBOrtalama[i], StratejikToplamTutar[i], OngorulenKBOrtalama[i], OngorulenKBBakiye[i], KarOran, StratejikNetKazanc[i], KarOran, BeklentiNetKazanc[i]);

        }
        /////////////////////
        //for (int i = 0; i < ProtokolAy; i++)
        //{
        //    dtContentGridFooter.Rows.Add(i + 1, 0, 0, 0, 0, 0, 0, KarOran, 0, KarOran, 0);
        //}
    }
}

public class Sema4
{
    public DataTable dtContentGridHeader;
    public DataTable dtContentPanel;
    public DataTable dtContentGridFooter;
    public Sema4(string title, string Kod, int ProtokolAy, decimal ToplamOrtalamaBireyselKredi, decimal TKGenelKarsilikOran, decimal TCMBPolitikaFaizOran, DataTable SemaCont)
    {
        dtContentGridHeader = new DataTable();
        dtContentGridHeader.Columns.Add("MevcutDurum", typeof(string));
        dtContentGridHeader.Columns.Add("Kod", typeof(string));
        dtContentGridHeader.Columns.Add("ToplamOrtalamaBireyselKredi", typeof(decimal));
        dtContentGridHeader.Rows.Add(title, Kod, ToplamOrtalamaBireyselKredi);

        dtContentPanel = new DataTable();
        dtContentPanel.Columns.Add("COL_COMP_ID", typeof(string));
        dtContentPanel.Columns.Add("COL_VAL", typeof(decimal));
        dtContentPanel.Rows.Add("NumberFieldTKGenelKarsilikOran", TKGenelKarsilikOran);
        dtContentPanel.Rows.Add("NumberFieldTCMBPolitikaFaizOran", TCMBPolitikaFaizOran);

        if (SemaCont.Rows.Count > 0)
        {
            dtContentGridFooter = SemaCont;
        }
        else
        {
            dtContentGridFooter = new DataTable();
            dtContentGridFooter.Columns.Add("Ay", typeof(int));
            dtContentGridFooter.Columns.Add("StratejikToplamTutar", typeof(decimal));
            dtContentGridFooter.Columns.Add("OngorulenOrtalamaBakiye", typeof(decimal));
            dtContentGridFooter.Columns.Add("StratejikKarsilikTutar", typeof(decimal));
            dtContentGridFooter.Columns.Add("OngorulenOrtalamakKarsilikTutar", typeof(decimal));
            dtContentGridFooter.Columns.Add("StratejikNetKazanc", typeof(decimal));
            dtContentGridFooter.Columns.Add("BeklentiNetKazanc", typeof(decimal));

            for (int i = 0; i < ProtokolAy; i++)
            {
                dtContentGridFooter.Rows.Add(i + 1, 0, 0, 0, 0, 0, 0);
            }
        }
    }
}

public class Sema5
{
    public DataTable dtContentGridHeader;
    public DataTable dtContentPanel;
    public DataTable dtContentGridFooter;
    public Sema5(string title, string Kod, int ProtokolAy, decimal ToplamOrtalamaBireyselKredi, decimal OzelKarsiliklarKarsilikOran, decimal OzelKarsiliklarTakipOran, decimal TCMBPolitikaFaizOran, DataTable SemaContent)
    {
        dtContentGridHeader = new DataTable();
        dtContentGridHeader.Columns.Add("MevcutDurum", typeof(string));
        dtContentGridHeader.Columns.Add("Kod", typeof(string));
        dtContentGridHeader.Columns.Add("ToplamOrtalamaBireyselKredi", typeof(decimal));
        dtContentGridHeader.Rows.Add(title, Kod, ToplamOrtalamaBireyselKredi);

        dtContentPanel = new DataTable();
        dtContentPanel.Columns.Add("COL_COMP_ID", typeof(string));
        dtContentPanel.Columns.Add("COL_VAL", typeof(decimal));
        dtContentPanel.Rows.Add("NumberFieldOzelKarsiliklarKarsilikOran", OzelKarsiliklarKarsilikOran);
        dtContentPanel.Rows.Add("NumberFieldOzelKarsiliklarTakipOran", OzelKarsiliklarTakipOran);
        dtContentPanel.Rows.Add("NumberFieldTCMBPolitikaFaizOran", TCMBPolitikaFaizOran);

        dtContentGridFooter = new DataTable();
        dtContentGridFooter.Columns.Add("Ay", typeof(int));
        dtContentGridFooter.Columns.Add("StratejikToplamTutar", typeof(decimal));
        dtContentGridFooter.Columns.Add("OngorulenOrtalamaBakiye", typeof(decimal));
        dtContentGridFooter.Columns.Add("StratejikKarsilikTutar", typeof(decimal));
        dtContentGridFooter.Columns.Add("OngorulenOrtalamakKarsilikTutar", typeof(decimal));
        dtContentGridFooter.Columns.Add("StratejikNetKazanc", typeof(decimal));
        dtContentGridFooter.Columns.Add("BeklentiNetKazanc", typeof(decimal));

        if (SemaContent.Rows.Count > 0)
        {
            dtContentGridFooter = SemaContent;
        }
        else
        {
            for (int i = 0; i < ProtokolAy; i++)
            {
                dtContentGridFooter.Rows.Add(i + 1, 0, 0, 0, 0, 0, 0);
            }
        }
    }
}

public class Sema6
{
    public DataTable dtContentGridHeader;
    public DataTable dtContentPanel;
    public DataTable dtContentGridFooter;
    public Sema6(string title, string Kod, int ProtokolAy, decimal ToplamOrtalamaBireyselKredi, decimal TakipOnlemePayi)
    {
        dtContentGridHeader = new DataTable();
        dtContentGridHeader.Columns.Add("MevcutDurum", typeof(string));
        dtContentGridHeader.Columns.Add("Kod", typeof(string));
        dtContentGridHeader.Columns.Add("ToplamOrtalama", typeof(decimal));
        dtContentGridHeader.Rows.Add(title, Kod, ToplamOrtalamaBireyselKredi);

        dtContentPanel = new DataTable();
        dtContentPanel.Columns.Add("COL_COMP_ID", typeof(string));
        dtContentPanel.Columns.Add("COL_VAL", typeof(decimal));
        dtContentPanel.Rows.Add("NumberFieldTOP", TakipOnlemePayi);

        dtContentGridFooter = new DataTable();
        dtContentGridFooter.Columns.Add("Ay", typeof(int));
        dtContentGridFooter.Columns.Add("KarTutar", typeof(decimal));

        decimal KarTutar = (ToplamOrtalamaBireyselKredi * TakipOnlemePayi / 100) / ProtokolAy;
        for (int i = 0; i < ProtokolAy; i++)
        {
            dtContentGridFooter.Rows.Add(i + 1, KarTutar);
        }
    }
}

public class Sema7
{
    public DataTable dtContentGridHeader;
    public DataTable dtContentPanel;
    public DataTable dtContentGridFooter;
    public Sema7(string title, string Kod, int ProtokolAy, decimal KisiBasiAylikOrtalama, int PersonelAdet)
    {
        dtContentGridHeader = new DataTable();
        dtContentGridHeader.Columns.Add("MevcutDurum", typeof(string));
        dtContentGridHeader.Columns.Add("Kod", typeof(string));
        dtContentGridHeader.Columns.Add("KisiBasiAylikOrtalama", typeof(decimal));
        dtContentGridHeader.Rows.Add(title, Kod, KisiBasiAylikOrtalama);

        dtContentPanel = new DataTable();
        dtContentPanel.Columns.Add("COL_COMP_ID", typeof(string));
        dtContentPanel.Columns.Add("COL_VAL", typeof(int));
        dtContentPanel.Rows.Add("NumberFieldPersonelAdet", PersonelAdet);

        dtContentGridFooter = new DataTable();
        dtContentGridFooter.Columns.Add("Ay", typeof(int));
        dtContentGridFooter.Columns.Add("KarTutar", typeof(decimal));

        decimal KarTutar = KisiBasiAylikOrtalama * PersonelAdet;
        for (int i = 0; i < ProtokolAy; i++)
        {
            dtContentGridFooter.Rows.Add(i + 1, KarTutar);
        }
    }
}

public class Sema8
{
    public DataTable dtContentGridHeader;
    public DataTable dtContentPanel;
    public DataTable dtContentGridFooter;
    public Sema8(string title, string Kod, int ProtokolAy, int AylikIslemAdet, decimal BirimMaliyet)
    {
        decimal ProjeMaliyetOngoru = AylikIslemAdet * BirimMaliyet;
        dtContentGridHeader = new DataTable();
        dtContentGridHeader.Columns.Add("MevcutDurum", typeof(string));
        dtContentGridHeader.Columns.Add("Kod", typeof(string));
        dtContentGridHeader.Columns.Add("AylikIslemAdet", typeof(int));
        dtContentGridHeader.Columns.Add("BirimMaliyet", typeof(decimal));
        dtContentGridHeader.Rows.Add(title, Kod, AylikIslemAdet, BirimMaliyet);

        dtContentPanel = new DataTable();
        dtContentPanel.Columns.Add("COL_COMP_ID", typeof(string));
        dtContentPanel.Columns.Add("COL_VAL", typeof(decimal));
        //dtContentPanel.Rows.Add("NumberFieldAylikIslemAdet", AylikIslemAdet);
        //dtContentPanel.Rows.Add("NumberFieldBirimMaliyet", BirimMaliyet);

        dtContentGridFooter = new DataTable();
        dtContentGridFooter.Columns.Add("Ay", typeof(int));
        dtContentGridFooter.Columns.Add("AylikMaliyet", typeof(decimal));

        for (int i = 0; i < ProtokolAy; i++)
        {
            dtContentGridFooter.Rows.Add(i + 1, ProjeMaliyetOngoru);
        }
    }
}

public class Sema9
{
    public DataTable dtContentGridHeader;
    public DataTable dtContentPanel;
    public DataTable dtContentGridFooter;
    public string[] CustomTitleArray;
    public Sema9(string Kod, string title, int ProtokolAy, int AylikIslemAdet, decimal BirimMaliyet)
    {
        CustomTitleArray = new string[2];
        if (Kod == "AMM")
        {
            CustomTitleArray[0] = "ATM Adedi";
            CustomTitleArray[1] = "ATM Maliyeti (TL/ATM)";
        }
        if (Kod == "USM")
        {
            CustomTitleArray[0] = "Uydu Şube Adedi";
            CustomTitleArray[1] = "Uydu Şube Maliyeti (TL/Uydu Şube)";
        }
        decimal ProjeMaliyetOngoru = AylikIslemAdet * BirimMaliyet;
        dtContentGridHeader = new DataTable();
        dtContentGridHeader.Columns.Add("MevcutDurum", typeof(string));
        dtContentGridHeader.Columns.Add("Kod", typeof(string));
        dtContentGridHeader.Columns.Add("AylikIslemAdet", typeof(int));
        dtContentGridHeader.Columns.Add("BirimMaliyet", typeof(decimal));
        dtContentGridHeader.Rows.Add(title, Kod, AylikIslemAdet, BirimMaliyet);

        dtContentPanel = new DataTable();
        dtContentPanel.Columns.Add("COL_COMP_ID", typeof(string));
        dtContentPanel.Columns.Add("COL_VAL", typeof(decimal));
        //dtContentPanel.Rows.Add("NumberFieldAylikIslemAdet", AylikIslemAdet);
        //dtContentPanel.Rows.Add("NumberFieldBirimMaliyet", BirimMaliyet);

        dtContentGridFooter = new DataTable();
        dtContentGridFooter.Columns.Add("Ay", typeof(int));
        dtContentGridFooter.Columns.Add("AylikOrtalamaMaliyet", typeof(decimal));

        for (int i = 0; i < ProtokolAy; i++)
        {
            dtContentGridFooter.Rows.Add(i + 1, BirimMaliyet * AylikIslemAdet / 12);
        }
    }
}

public class Sema10
{
    public DataTable dtContentGridHeader;
    public DataTable dtContentPanel;
    public DataTable dtContentGridFooter;
    public Sema10(string title, string Kod, int ProtokolAy, decimal KisiBasiAylikOrtalama, int PersonelAdet)
    {
        dtContentGridHeader = new DataTable();
        dtContentGridHeader.Columns.Add("MevcutDurum", typeof(string));
        dtContentGridHeader.Columns.Add("Kod", typeof(string));
        dtContentGridHeader.Columns.Add("KisiBasiAylikOrtalama", typeof(decimal));
        dtContentGridHeader.Rows.Add(title, Kod, KisiBasiAylikOrtalama);

        dtContentPanel = new DataTable();
        dtContentPanel.Columns.Add("COL_COMP_ID", typeof(string));
        dtContentPanel.Columns.Add("COL_VAL", typeof(int));
        dtContentPanel.Rows.Add("NumberFieldPersonelAdet", PersonelAdet);

        dtContentGridFooter = new DataTable();
        dtContentGridFooter.Columns.Add("Ay", typeof(int));
        dtContentGridFooter.Columns.Add("KarTutar", typeof(decimal));

        decimal KarTutar = KisiBasiAylikOrtalama * PersonelAdet;
        for (int i = 0; i < ProtokolAy; i++)
        {
            dtContentGridFooter.Rows.Add(i + 1, KarTutar);
        }
    }
}


public class WindowUtility : IDisposable
{
    DataAccess dataAccess = new DataAccess();
    Window window;
    List<GridPanel> gridList;
    List<Panel> panelList;
    Sema sema;
    int GridIndex;
    int PanelIndex;

    public WindowUtility(Window windowToFill, List<GridPanel> gridList_, List<Panel> panelList_, Sema Sema_)
    {
        window = windowToFill;
        gridList = gridList_;
        panelList = panelList_;
        sema = Sema_;
        GridIndex = 0;
        PanelIndex = 0;
    }

    public DataTable getContent()
    {
        return null;
    }

    public void newGridPanel(string ParentID)
    {
        DataTable dtContent = new DataTable();
        if (GridIndex == 0)
        {
            dtContent = sema.dtContentGridHeader;
        }
        else if (GridIndex == 1)
        {
            dtContent = sema.dtContentGridFooter;
        }
        else
        {
            //hata
        }
        DataTable dtProperties;
        using (GridPanelUtility gpu = new GridPanelUtility())
        {
            dtProperties = gpu.getColumns(ParentID);
            gpu.FillGridwithDatatable(gridList[GridIndex], dtProperties, dtContent, false);
        }
        GridIndex = GridIndex + 1;
    }

    public void newPanel(string ParentID)
    {
        DataTable dtContent = sema.dtContentPanel;
        PanelFieldUtility pu = new PanelFieldUtility();

        SqlParameter[] sqlparamArrayProp = new SqlParameter[4] { new SqlParameter("@InputComponent", ParentID), new SqlParameter("@InputTypeof", "Field"), new SqlParameter("@MinRecursionLevel", "0"), new SqlParameter("@MaxRecursionLevel", "9999") };
        DataTable dtProperties = dataAccess.getSQL("getProProperties", sqlparamArrayProp);

        Ext.Net.Panel[] panels = new Ext.Net.Panel[1];
        panels[PanelIndex] = panelList[PanelIndex];
        pu.FillPanelswithFields(panels, dtContent, dtProperties, "COL_COMP_ID", "COL_VAL", sema.CustomTitleArray, sema.Kod);
        //X.GetCmp<Panel>(panelList[PanelIndex].ID).UpdateLayout();
        PanelIndex = PanelIndex + 1;
    }

    public void newWindow(string Kod)
    {
        string WindowID = sema.WindowID;
        //X.GetCmp<Panel>(panelList[1].ID).Hidden = sema.hidden;

        SqlParameter[] sqlparamArrayProp = new SqlParameter[4] { new SqlParameter("@InputComponent", WindowID), new SqlParameter("@InputTypeof", " "), new SqlParameter("@MinRecursionLevel", "0"), new SqlParameter("@MaxRecursionLevel", "9999") };
        DataTable dtProperties = dataAccess.getSQL("getProProperties", sqlparamArrayProp);
        GridPanelUtility gu = new GridPanelUtility();
        PanelFieldUtility pu = new PanelFieldUtility();
        if (dtProperties.Rows.Count > 0)
        {
            DataTable MainComponents = dtProperties.Select("RecursionLevel=0").CopyToDataTable();
            if (MainComponents.Rows.Count > 0)
            {
                for (int i = 0; i < MainComponents.Rows.Count; i++)
                {
                    string componentType = CommonFunctions.FieldOrDefault<string>(MainComponents, MainComponents.Rows[i], "TYPEOF");
                    string componentID = CommonFunctions.FieldOrDefault<string>(MainComponents, MainComponents.Rows[i], "COMPONENT_ID");

                    switch (componentType)
                    {
                        case "Gridpanel":
                            newGridPanel(componentID);
                            break;
                        case "Panel":
                            newPanel(componentID);
                            break;
                        case " ":
                            //hata ver
                            break;
                        default:
                            //hata ver
                            break;
                    }
                }
            }
            else
            {
                //hata ver
            }
        }
        else
        {
            //hata ver
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}