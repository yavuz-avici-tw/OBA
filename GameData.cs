using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OBA
{
    internal class GameData
    {
        public const string gameXML = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\r\n<ENCOUNTERS>\r\n\t<ENCOUNTER id=\"1\">\r\n\t\t<CHARACTER>ŞAMAN</CHARACTER>\r\n\t\t<TEXT>Halk obada dolaşan hortlaklardan rahatsız, herkesi toplayıp büyük bir ayin düzenlememiz lazım.</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Hazırlıklara başlayın</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>1</FAITH>\r\n\t\t\t\t<PEOPLE>0.5</PEOPLE>\r\n\t\t\t\t<SECURITY>0</SECURITY>\r\n\t\t\t\t<MONEY>-1</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t<FIRE_ENCOUNTER>4</FIRE_ENCOUNTER>\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Saçma sapan söylentiler…</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>1</FAITH>\r\n\t\t\t\t<PEOPLE>0.5</PEOPLE>\r\n\t\t\t\t<SECURITY>0</SECURITY>\r\n\t\t\t\t<MONEY>-1</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t<FIRE_ENCOUNTER>NONE</FIRE_ENCOUNTER>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"2\">\r\n\t\t<CHARACTER>ORDU_BAŞI</CHARACTER>\r\n\t\t<TEXT>Karşı beyliğin yeni kılıçlar dövdüğünü duydum, biz de demircilerimize yatırım yapmalıyız.</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Hemen.</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0</FAITH>\r\n\t\t\t\t<PEOPLE>0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.5</SECURITY>\r\n\t\t\t\t<MONEY>-0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t<FIRE_ENCOUNTER>3</FIRE_ENCOUNTER>\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Gerek yok</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0</FAITH>\r\n\t\t\t\t<PEOPLE>0</PEOPLE>\r\n\t\t\t\t<SECURITY>-0.5</SECURITY>\r\n\t\t\t\t<MONEY>0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t<FIRE_ENCOUNTER>NONE</FIRE_ENCOUNTER>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\t<ENCOUNTER id=\"3\" continuation=\"true\">\r\n\t\t<CHARACTER>AŞÇI</CHARACTER>\r\n\t\t<TEXT>Beyim, obanın bu paraya ihtiyacı var, kılıçlar yerine fakirlerin karnını doyurmak için çorba kazanı açalım</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Haklısın</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0</FAITH>\r\n\t\t\t\t<PEOPLE>0.5</PEOPLE>\r\n\t\t\t\t<SECURITY>-0.5</SECURITY>\r\n\t\t\t\t<MONEY>0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t<FIRE_ENCOUNTER>NONE</FIRE_ENCOUNTER>\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Hayır</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0</FAITH>\r\n\t\t\t\t<PEOPLE>0</PEOPLE>\r\n\t\t\t\t<SECURITY>0</SECURITY>\r\n\t\t\t\t<MONEY>0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t<FIRE_ENCOUNTER>NONE</FIRE_ENCOUNTER>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\r\n\t<ENCOUNTER id=\"4\">\r\n\t\t<CHARACTER>ŞAMAN</CHARACTER>\r\n\t\t<TEXT>Beyim, al karısı obadaki bir kadına musallat olmuş, yanına 40 gün boyunca yalnız kalmaması lazım.</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>Askerlerden birini gönder</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>-0.5</SECURITY>\r\n\t\t\t\t<MONEY>0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t<FIRE_ENCOUNTER>NONE</FIRE_ENCOUNTER>\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Sen ilgilen</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.5</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0</SECURITY>\r\n\t\t\t\t<MONEY>-0.5</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t<FIRE_ENCOUNTER>NONE</FIRE_ENCOUNTER>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n\r\n\t<ENCOUNTER id=\"5\">\r\n\t\t<CHARACTER>ŞAMAN</CHARACTER>\r\n\t\t<TEXT>Beyim, obaya büyük bir mabed yaparsak halkımızın inancını güçlü tutarız, en zor zamanlarda bile.</TEXT>\r\n\t\t<YES>\r\n\t\t\t<TEXT>İnşaata başlayın</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.5</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0.0</SECURITY>\r\n\t\t\t\t<MONEY>-2.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t<FIRE_ENCOUNTER>NONE</FIRE_ENCOUNTER>\r\n\t\t\t<CONSTRUCTION>construction_temple</CONSTRUCTION>\r\n\t\t</YES>\r\n\t\t<NO>\r\n\t\t\t<TEXT>Hayır</TEXT>\r\n\t\t\t<STAT_CHANGE>\r\n\t\t\t\t<FAITH>0.0</FAITH>\r\n\t\t\t\t<PEOPLE>0.0</PEOPLE>\r\n\t\t\t\t<SECURITY>0</SECURITY>\r\n\t\t\t\t<MONEY>0.0</MONEY>\r\n\t\t\t</STAT_CHANGE>\r\n\t\t\t<FIRE_ENCOUNTER>NONE</FIRE_ENCOUNTER>\r\n\t\t</NO>\r\n\t</ENCOUNTER>\r\n    \r\n</ENCOUNTERS>\r\n";
        
        public static List<Encounter> getEncountersFromXmlData(string xmlData)
        {
            XDocument xdoc = XDocument.Parse(xmlData);

            List<Encounter> encounters = new List<Encounter>();

            IEnumerable<XElement> elmns = xdoc.Descendants("ENCOUNTER");

            foreach (XElement elm in elmns)
            {

                Encounter enc = parseEncounter(elm);
                encounters.Add(enc);

            }
            return encounters;
        }
        private static Encounter parseEncounter(XElement encounterElement)
        {
            int id;
            bool isLocked;
            bool isContinuation;
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
            return new Encounter(id, character, text,  yes, no, isLocked, isContinuation);
        }
        private static Action parseAction(XElement actionElement)
        {
            string text = actionElement.Element("TEXT")?.Value.ToString() ?? actionElement.Name.ToString();
            float stat_faith;
            float stat_people;
            float stat_security;
            float stat_money;
            int fire_encounter;

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
            // int fire_y_id = int.Parse(elm.Element("YES")?.Element("FIRE_ENCOUNTER")?.Value);
            return new Action(text, new StatChange(stat_faith, stat_people, stat_security, stat_money));
        }
    }
}
