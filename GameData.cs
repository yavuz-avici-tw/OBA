using System.Collections.Frozen;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace OBA
{
    public class GameData
    {
        //LowFaith, HighFaith, LowPeople, HighPeople, LowMoney, HighMoney, LowSecurity, HighSecurity
        public static readonly FrozenDictionary<GameState.GameEndReason,string> GameEndReasonTexts = new Dictionary<GameState.GameEndReason, string>
        {
            {GameState.GameEndReason.LowFaith, "Şaman seni inançsızlıkla suçladı ve insanları da arkasına alarak kelleni obada gezdirdi. Kaybettin." },
            {GameState.GameEndReason.HighFaith, "Şamanın elindeki güç çok arttı ve obanın yönetimini eline aldı, artık sen sadece onun bir kuklasısın. Kaybettin."},
            {GameState.GameEndReason.LowPeople, "Halkın sersefil kaldı ve bulamadığı at etini senin karnından deşip çıkarmaya karar verdi. Kaybettin"},
            {GameState.GameEndReason.HighPeople, "Halkın refahı arttıkça istekleri de bitmedi, gücü elinde tutamadığın için senin yerini anarşi aldı. Kaybettin."},
            {GameState.GameEndReason.LowMoney, "Hazinede bi' bok kalmadı, ne obana ne kendine yetecek bir şey yok, esnaflar birleşip seni indirme kararı aldılar. Kaybettin"},
            {GameState.GameEndReason.HighMoney,"Zenginliğin etrafındakilerin gözüne batmaya başladı, para ve şöhret hırsıyla seni yerinden etmek isteyenlerin ardı arkası kesilmedi. Kaybettin"},
            {GameState.GameEndReason.LowSecurity, "Beyliğinde güvenlik namına bir şey kalmadı, etrafındaki bütün beylikler pastadan birer parça aldı. Kaybettin."},
            {GameState.GameEndReason.HighSecurity, "Ordu Başı senin ve gereksiz bürokrasinin sonunu getirmeye karar verdi ve gücü eline aldı. Kaybettin." }

        }.ToFrozenDictionary();

        private const string _gameXML = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\r\n<ENCOUNTERS>\r\n\t<ENCOUNTER id=\"1\">\r\n\t\t<CHARACTER>ŞAMAN</CHARACTER>\r\n\t\t<TEXT>Halk obada dolaşan hortlaklardan rahatsız, herkesi toplayıp büyük bir ayin düzenlememiz lazım.</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Hazırlıklara başlayın</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>1</FAITH>\r\n\t\t\t\t<PEOPLE>0.5</PEOPLE>\r\n\t\t\t\t<SECURITY>0</SECURITY>\r\n\t\t\t\t<MONEY>-1</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t<FIRE_ENCOUNTER>4</FIRE_ENCOUNTER>\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Saçma sapan söylentiler…</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>1</FAITH>\r\n\t\t\t\t<PEOPLE>0.5</PEOPLE>\r\n\t\t\t\t<SECURITY>0</SECURITY>\r\n\t\t\t\t<MONEY>-1</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"2\">\r\n\t\t<CHARACTER>ORDU BAŞI</CHARACTER>\r\n\t\t<TEXT>Karşı beyliğin yeni kılıçlar dövdüğünü duydum, biz de demircilerimize yatırım yapmalıyız.</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Hemen.</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0</FAITH>\r\n\t\t\t\t<PEOPLE>0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.5</SECURITY>\r\n\t\t\t\t<MONEY>-0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t<FIRE_ENCOUNTER>3</FIRE_ENCOUNTER>\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Gerek yok</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0</FAITH>\r\n\t\t\t\t<PEOPLE>0</PEOPLE>\r\n\t\t\t\t<SECURITY>-0.5</SECURITY>\r\n\t\t\t\t<MONEY>0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"3\" continuation=\"true\">\r\n\t\t<CHARACTER>AŞÇI</CHARACTER>\r\n\t\t<TEXT>Beyim, obanın bu paraya ihtiyacı var, kılıçlar yerine fakirlerin karnını doyurmak için çorba kazanı açalım</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Haklısın</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0</FAITH>\r\n\t\t\t\t<PEOPLE>0.5</PEOPLE>\r\n\t\t\t\t<SECURITY>-0.5</SECURITY>\r\n\t\t\t\t<MONEY>0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Hayır</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0</FAITH>\r\n\t\t\t\t<PEOPLE>0</PEOPLE>\r\n\t\t\t\t<SECURITY>0</SECURITY>\r\n\t\t\t\t<MONEY>0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\r\n\t<ENCOUNTER id=\"4\">\r\n\t\t<CHARACTER>ŞAMAN</CHARACTER>\r\n\t\t<TEXT>Beyim, al karısı obadaki bir kadına musallat olmuş, yanına 40 gün boyunca yalnız kalmaması lazım.</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Askerlerden birini gönder</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>-0.5</SECURITY>\r\n\t\t\t\t<MONEY>0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Sen ilgilen</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.5</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0</SECURITY>\r\n\t\t\t\t<MONEY>-0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\r\n\t<ENCOUNTER id=\"5\">\r\n\t\t<CHARACTER>ŞAMAN</CHARACTER>\r\n\t\t<TEXT>Beyim, obaya büyük bir mabed yaparsak halkımızın inancını güçlü tutarız, en zor zamanlarda bile.</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>İnşaata başlayın</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.5</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>-2.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t\t<CONSTRUCTION>construction_temple</CONSTRUCTION>\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Hayır</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n    \r\n\t<ENCOUNTER id=\"6\">\r\n\t\t<CHARACTER>SEYİS</CHARACTER>\r\n\t\t<TEXT>Atların diri kalması için tahılımızın bir kısmını ayırmamız lazım.</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Tamam</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>-0.5</PEOPLE>\r\n\t\t\t\t<SECURITY>0.5</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Hayır, obanın ihtiyacı var</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.5</PEOPLE>\r\n\t\t\t\t<SECURITY>-0.5</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\r\n\t<ENCOUNTER id=\"7\">\r\n\t\t<CHARACTER>HATUN</CHARACTER>\r\n\t\t<TEXT>Beyim... bu akşamki toy için hangi elbiseyi giyeyim?</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Mavi</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Kırmızı</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"8\">\r\n\t\t<CHARACTER>ORDU BAŞI</CHARACTER>\r\n\t\t<TEXT>Askerlerimizin birçoğu yaşlı, genç nüfusun ilgisini çekmeliyiz</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Haklısın.</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0</FAITH>\r\n\t\t\t\t<PEOPLE>0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.5</SECURITY>\r\n\t\t\t\t<MONEY>-0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Gerek yok</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0</FAITH>\r\n\t\t\t\t<PEOPLE>0.5</PEOPLE>\r\n\t\t\t\t<SECURITY>-0.5</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"9\">\r\n\t\t<CHARACTER>HALKTAN BİRİ</CHARACTER>\r\n\t\t<TEXT>Beyim, din adamları sürekli bağış topluyor ancak halkın karnı hala aç</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Haklısın.</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>-1.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.5</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Din kutsaldır</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.5</FAITH>\r\n\t\t\t\t<PEOPLE>-1.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"10\">\r\n\t\t<CHARACTER>VALİ</CHARACTER>\r\n\t\t<TEXT>İşçiler greve girdi, tez vakitte maaşlarını istiyorlar</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Akçe kesemi getirin</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>1.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>-0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Askerleri salın</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>-1.0</PEOPLE>\r\n\t\t\t\t<SECURITY>-0.5</SECURITY>\r\n\t\t\t\t<MONEY>1.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"11\">\r\n\t\t<CHARACTER>VALİ</CHARACTER>\r\n\t\t<TEXT>Acil durum. Obadaki çadırlar alev almış, ortalık karışık</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Sivillere yardım edin</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>-0.5</SECURITY>\r\n\t\t\t\t<MONEY>-1.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Alevi söndürün</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>-1.0</PEOPLE>\r\n\t\t\t\t<SECURITY>-0.5</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"12\">\r\n\t\t<CHARACTER>VALİ</CHARACTER>\r\n\t\t<TEXT>Diğer beyliklerden birinin köyü talan edilmiş, bir grup göçmen bize doğru akın ediyor</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Gelsinler</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>1.0</PEOPLE>\r\n\t\t\t\t<SECURITY>-0.5</SECURITY>\r\n\t\t\t\t<MONEY>-0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Dışarıda tutun</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>-0.5</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"13\">\r\n\t\t<CHARACTER>AŞÇI</CHARACTER>\r\n\t\t<TEXT>Beyim, kımız stoklarımız bitmek üzere, yenilensin ister misiniz? Ordu başı sürekli kımız istiyor.</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Olur</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.5</SECURITY>\r\n\t\t\t\t<MONEY>-0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Hayır</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"14\">\r\n\t\t<CHARACTER>VALİ</CHARACTER>\r\n\t\t<TEXT>Bu ay toprak mahsulü iyi, tımarların vergilerini arttıralım mı?</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Birazcık.</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>-0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>İki katına çıkarın</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>-0.5</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>1.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"15\">\r\n\t\t<CHARACTER>ORDU BAŞI</CHARACTER>\r\n\t\t<TEXT>Savaşta esir düşürdüğümüz askerlerle napalım?</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Köle pazarında satın.</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Bildiklerine ihtiyacımız var</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.5</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"15\">\r\n\t\t<CHARACTER>ORDU BAŞI</CHARACTER>\r\n\t\t<TEXT>Savaşta esir düşürdüğümüz askerlerle napalım?</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Köle pazarında satın.</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Bildiklerine ihtiyacımız var</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.5</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"16\">\r\n\t\t<CHARACTER>ŞAMAN</CHARACTER>\r\n\t\t<TEXT>Dini turizm bize son zamanlarda iyi gelir sağladı, yatırımı nereye yapalım?</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Hepsi hazineye</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>1.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Yatırımı paylaştıralım</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.5</FAITH>\r\n\t\t\t\t<PEOPLE>0.5</PEOPLE>\r\n\t\t\t\t<SECURITY>0.5</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"17\">\r\n\t\t<CHARACTER>ŞAMAN</CHARACTER>\r\n\t\t<TEXT>Halk arasında yeni bir peygamber olduğunu söyleyerek dolaşan biri var, bu olaya bir el atmamız lazım.</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Sallandırın</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>1.0</FAITH>\r\n\t\t\t\t<PEOPLE>-1.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Bırakın dolaşsın</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>-1.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.5</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"18\">\r\n\t\t<CHARACTER>VALİ</CHARACTER>\r\n\t\t<TEXT>Halkı salgın tutmuş, bir şeyler yapmazsak bütün obayı kaybedeceğiz</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Ortadan kaldırın</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>-1.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Şifacı şamanları getirin</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>-0.5</FAITH>\r\n\t\t\t\t<PEOPLE>0.5</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>-0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"19\">\r\n\t\t<CHARACTER>ŞAMAN</CHARACTER>\r\n\t\t<TEXT>Karanlık... geleceğimizde karanlık şeyler görüyorum...</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Saçmalama</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>-0.5</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Sana lazım olan şeyi biliyorum</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>-0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"20\">\r\n\t\t<CHARACTER>VALİ</CHARACTER>\r\n\t\t<TEXT>Tüccarlar buraya gelip şaman'ı rahatsız edecek şeyler satıyorlar</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Umrumda değil</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>-0.5</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>1.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Ona da payını ver</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"21\">\r\n\t\t<CHARACTER>VALİ</CHARACTER>\r\n\t\t<TEXT>Kaybettiğimiz askerlerin anısına büyük bir tören hazırlamayı düşünüyoruz</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Tamam</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>1.0</SECURITY>\r\n\t\t\t\t<MONEY>-1.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Hayır</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>-0.5</SECURITY>\r\n\t\t\t\t<MONEY>0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"22\">\r\n\t\t<CHARACTER>ELÇİ</CHARACTER>\r\n\t\t<TEXT>Karşı beylik cüzi bir miktar karşılığı bizden koruma talep ediyor, kabul edersek insanlarımız rahatsız olabilir</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Askerleri gönderin.</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>-0.5</PEOPLE>\r\n\t\t\t\t<SECURITY>-0.5</SECURITY>\r\n\t\t\t\t<MONEY>1.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Bir şey yapamayız.</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.5</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\r\n\t<ENCOUNTER id=\"23\" probabilityModifier=\"0.1\" isOneTime=\"true\">\r\n\t\t<CHARACTER>ELÇİ</CHARACTER>\r\n\t\t<TEXT>Karşı beylik askeri ve ekonomik müttefiklik teklif ediyor, bu harika bir fırsat olabilir</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>İyi</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t<UNLOCK_ENCOUNTERS>\r\n\t\t\t\t<ENCOUNTER_ID>41131</ENCOUNTER_ID>\r\n\t\t\t</UNLOCK_ENCOUNTERS>\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Onlara güvenemeyiz</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\r\n\t<ENCOUNTER id=\"41131\">\r\n\t\t<CHARACTER>MÜTTEFİK BEYLİK</CHARACTER>\r\n\t\t<TEXT>Az uzağımızdaki beylik hali hazırda savaşta, pastadan biz de bir dilim almalıyız.</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Askerleri gönderin.</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>-0.5</SECURITY>\r\n\t\t\t\t<MONEY>1.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Bu doğru değil</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.5</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>-0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t\r\n\t\r\n</ENCOUNTERS>\r\n";
        
        internal static ReadOnlyCollection<Encounter> getEncountersFromXmlData(string xmlData = _gameXML)
        {
            XDocument xdoc = XDocument.Parse(xmlData);

            List<Encounter> encounters = new List<Encounter>();

            IEnumerable<XElement> elmns = xdoc.Descendants("ENCOUNTER");

            foreach (XElement elm in elmns)
            {

                Encounter enc = parseEncounter(elm);
                encounters.Add(enc);

            }
            return encounters.AsReadOnly();
        }

        private static Encounter parseEncounter(XElement encounterElement)
        {
            int id;
            float probabilityModifier;
            bool isLocked;
            bool isContinuation;
            bool isOneTime;
            string? character;
            string? text;
            if (!int.TryParse(encounterElement.Attribute("id")?.Value.ToString(), out id))
            {
                Console.Error.WriteLine("ERROR::PARSING::ENCOUNTER::ID_IS_A_MUST");
                System.Environment.Exit(-1);
            }
            if (!bool.TryParse(encounterElement.Attribute("locked")?.Value.ToString(), out isLocked)) { 
                isLocked = false;
            }
            if (!bool.TryParse(encounterElement.Attribute("continuation")?.Value.ToString(), out isContinuation))
            {
                isContinuation = false;
            }
            if (!bool.TryParse(encounterElement.Attribute("isOneTime")?.Value.ToString(), out isOneTime))
            {
                isOneTime = false;
            }
            if (!float.TryParse(encounterElement.Attribute("probabilityModifier")?.Value, out probabilityModifier))
            {
                probabilityModifier = 1.0f;
            }
            character = encounterElement.Element("CHARACTER")?.Value.ToString();
            text = encounterElement.Element("TEXT")?.Value.ToString();
            if (character == null)
            {
                Console.Error.WriteLine("ERROR::PARSING::ENCOUNTER::ACTION::CHARACTER_NOT_FOUND");
                System.Environment.Exit(-1);
            }
            if (text == null)
            {
                Console.Error.WriteLine("ERROR::PARSING::ENCOUNTER::ACTION::TEXT_NOT_FOUND");
                System.Environment.Exit(-1);
            }

            XElement? yesElement = encounterElement.Element("YES");
            XElement? noElement = encounterElement.Element("NO");
            if (yesElement == null || noElement == null)
            {
                Console.Error.WriteLine("ERROR::PARSING::ENCOUNTER::ACTION::YES_NO_ELEMENT_NOT_FOUND");
                System.Environment.Exit(-1);
            }

            Action yes = parseAction(yesElement);
            Action no = parseAction(noElement);
            return new Encounter(id, character, text,  yes, no, probabilityModifier, isLocked, isContinuation, isOneTime);
        }
        private static Action parseAction(XElement actionElement)
        {
            string text = actionElement.Element("TEXT")?.Value.ToString() ?? actionElement.Name.ToString();
            float stat_faith;
            float stat_people;
            float stat_security;
            float stat_money;
            int fire_encounter;
            List<int>? unlockEncountersList = null;  

            if (!int.TryParse(actionElement.Element("FIRE_ENCOUNTER")?.Value.ToString(), out fire_encounter))
            {
                fire_encounter = -1;
            }
            if (!float.TryParse(actionElement.Element("STAT_CHANGE")?.Element("FAITH")?.Value, out stat_faith))
            {
                Console.Error.WriteLine("ERROR::PARSING::ENCOUNTER::ACTION::FAITH_MUST_BE_A_FLOATING_POINT_NUMBER");
                System.Environment.Exit(-1);
            }
            if (!float.TryParse(actionElement.Element("STAT_CHANGE")?.Element("PEOPLE")?.Value, out stat_people))
            {
                Console.Error.WriteLine("ERROR::PARSING::ENCOUNTER::ACTION::PEOPLE_MUST_BE_A_FLOATING_POINT_NUMBER");
                System.Environment.Exit(-1);
            }
            if (!float.TryParse(actionElement.Element("STAT_CHANGE")?.Element("SECURITY")?.Value, out stat_security))
            {
                Console.Error.WriteLine("ERROR::PARSING::ENCOUNTER::ACTION::SECURITY_MUST_BE_A_FLOATING_POINT_NUMBER");
                System.Environment.Exit(-1);
            }
            if (!float.TryParse(actionElement.Element("STAT_CHANGE")?.Element("MONEY")?.Value, out stat_money))
            {
                Console.Error.WriteLine("ERROR::PARSING::ENCOUNTER::ACTION::MONEY_MUST_BE_A_FLOATING_POINT_NUMBER");
                System.Environment.Exit(-1);
            }
            XElement? unlockEncountersElement = actionElement.Element("UNLOCK_ENCOUNTERS") ?? null;
            if (unlockEncountersElement != null)
            {
                foreach (XElement item in unlockEncountersElement.Elements("ENCOUNTER_ID"))
                {
                    int unlockEncounterId;
                    if (!int.TryParse(item.Value.ToString(), out unlockEncounterId))
                    {
                        unlockEncounterId = -1;
                    }
                    if (unlockEncountersList != null)
                    {

                        unlockEncountersList.Add(unlockEncounterId);
                    }
                    else
                    {
                        unlockEncountersList = new List<int> { unlockEncounterId };
                    }
                }
            }
            // int fire_y_id = int.Parse(elm.Element("YES")?.Element("FIRE_ENCOUNTER")?.Value);
            return new Action(text, new StatChange(stat_faith, stat_people, stat_security, stat_money),unlockEncounters:unlockEncountersList?.AsReadOnly());
        }
    }
}
