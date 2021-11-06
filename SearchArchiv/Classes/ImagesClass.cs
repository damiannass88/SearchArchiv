using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SearchArchiv.Classes
{
    class ImagesClass
    {
        /// <summary>
        /// Class for return Base64 png string Image
        /// </summary>
        public static string SaxasImageBase64
        {
            // return image in form base64: "data:image/png;base64,"
            get
            {
                return "iVBORw0KGgoAAAANSUhEUgAAAEsAAAAhCAMAAACm/6bkAAAC91BMVEUAAAD39/f///+dnaMnI00iHUv+/v7////FxcbV1dU0MVRSUWVrannV1" +
                    "dW6ur28u7/9/fwuKlAkIEwvLFFEQlw0MVNEQlwxLVJxcX7///6vrrOfn6XBwcT8/Pw8OVgrJ09PTmQwLVJUU2c0MVROTGMwLFI8OVddXG1dXG49O1deXW9iYHCFhY6UlJyTkpqrqq+zs7ePj5evr7K" +
                    "3t7rGxsnAwMK5uLytrbL9/fzr6uq9vb78/fv///////85NlRIRl9ZWGtHRV8yL1JkYnRsanlcW211dIB5d4NqaXd+fYhoZ3aDg41mZXWUk5uIh5F8fIZ0c4GLi5SAf4mHh4+Yl554d4ODgo23t7qvr" +
                    "rK1tbnBwcS6ubyRkZi0tLikpKmzs7Xx8fDDw8f8/PzNzc7h4uG7ur+8vMArJ01CQFswLVE1MlQPBEVjYnM9O1h6eYRLSWA6OVh1dYEvK1FjYnNLSWJXVmlBP1o8Olc0MVNPTWVNTGJkY3RFQ1xvbnt" +
                    "NTF5mZXVcW2xnZXWMi5R9fYdtbHppaHeFhY9wb3xranhxcX9xcH2Eg453doKFhI5ubXt0dIGFhY54d4OVlJpxcH+KiZKenqR5eIWdnKKTk5uioqeMjJStrbGko6ihoaaPjpegn6e4uLuNjZWrqq+fn" +
                    "6XMzM7ExMW9vMDx8fHa2tzCwsXx8vD+/v3X19j////////S0tMbFkY3NVYeGUhZWGw7OVcsKFBHRF43NFVbWm1paHc2NFV/f4p4eIJ3d4JubXtZWGtmZXViYHB/follZHVTUmNZWGxdW2tvbnuKiZ" +
                    "Jzcn6NjZVdW22ZmJ99fIhmZXOdnKKgoKWYmJ6ioqiIh5GPjpSRkJfDw8akpKjAwMK4uLunp6uXlp29vcC4uLy+vcH////X19XHx8rBwcW6ur7l5OceGEocFkkdFkobFEkfGUoZEkkYEEgAAEMKAEU" +
                    "FAEQNAUUUC0cWDUgTCEcPBEYAAEIRBkYhG0oHAEQAAEAzMFIAAEEAAD8AADLVt0/kAAAA5XRSTlMABAIW/v4TDAoH8+O2LyckIv39+ff28u64GBMSDQr9+/j47+7t6+bg29jVw8KWjoBzXFlPSj4u" +
                    "IR8eGRYOCPfu5OTk3NbHxsDAv7e1s62mpZeKiYh6cXBmY1pXVlRJQzMyKyYlIyEd/v39+/v58u7s6uXl4+Lg4ODf3trRzsPAvr64s66urKqqqaehoJuXlJGPjYaEg4KCfnpybGlpY2NdXFlSUU5GQ" +
                    "kE0MyopKRIRDv789vT08ezq5+Ph4Nzc3NjVysfFxL+/urexr6iknJiMhIJ9enhtampnYFdQTkpIQkE5ODgWN3gMTgAABN1JREFUGBntwWWQVWUABuD3nLvd3csusMsGscSyQXd3d3eppIIKKG1Lo4Q" +
                    "t2N3d3d3xft+p2/fuLugPz1l1xhnHGcbxhz94Hpzz37o3KqoHgMKoqHvx7yjR8+phS95yUd9+rwG4vM3ai15Jhq3bnGgVZ+u+KWNfXNNr+UTY8jaZQkSk4J4sU5iVeQDU66zM/u3GTlVwNm7T3e6A" +
                    "po9RACjX6KR1A6Y16dSvVgDEbje0QKOn1UL8o8LqQyM/6wnHYYs2Y2sMbDNakoE3cEWQbDkDtrznNZLydFc4bhs1+sYU/FXs3PfiPUFPqG0P2F730Kb17wbbgjYkL6yrkGSbBbAllwjamjrCdjzVG" +
                    "/YsHTS5m4o/zN+ZETJJ6kWdAChlPlJSZiXBkeAn46pak74cOKoFKUn3KAAxIwySInim/IiCZnUbhWCziHEqcJch2RBBem6FY2qILBqQSj10Ao4PLcrFkQw+oQCuco3NtMjr0Wy0m2RmdpoUDdcow" +
                    "FQ39RarTLo/gCMqw6AMSJq9e8Ax3E3/qiXk8hQgqZWgjMtuScrIFNi69yPNgbckDTWKnzukAlcFKeMv89M9FM2Ge+hovAIOpczHtLfP1+meDsx9SJMtOtcc76vpVmfYkktJ934g8fak/BhAHWjSn7O" +
                    "vgWa8AkdnPx36TDhSFgum31Ah6e0AuNYKefpOoP0vltUJtprWpPnCJ5Mm3fhTAYDuF5Lu9p3SKSLugmNhK0FSe3wRHNNCZNwPCT6aWwBXmUb/o+2/vnvOtGtnwpZcSjIQDgSspQlJwKw40joxK460p" +
                    "sOxsESQFPGL4Ojgpb46d3yYcn13FAwxSdMT2WrEMRWO3NU6fyfFnlgcSyfNxLtb6/R2gOOojw69CxwJPmoblS9CZPZsqEfSdNqEFjGsEI6jj0QYmtAEqfXPU69roPHAHYkbSP9WOHKCdDTugC26QtJ" +
                    "su+BWQ7JlJwD7stMbDE1St8aiWc3Ed3a9ldDbTxHviskxKCNXFEeQcv09AFJSBaVGmlndASS21sm0XhmSTDugAqi7ae/uYSsF5cUqgJ8/7djx856xsdMjKda58ssFSUPQVjofwAQ3uezpItKaDOCr" +
                    "TNqkIGnsip0/8s2RVYU9lcNe6k/mAqheZjWtyFWVLg3Uylw1GYJ/yrwFUC7WyMc6xpG+bQA+SuWftE35J5d4rD61ivplmPr5dQC6riTdl4y5Mt5HrTK/S4DU3O6Aj2TRJGB2NmkOzN0gyX71UK7WqP" +
                    "s9YV+QFL1r5xYLRr707u7zDMqKQgD1pTpp6NRIY5vaoZGy1wUXrOlzPymuVNSJEaT3IEYHyPSbUDBEo7/4qZztg8Ok/q2rjyANoQnSezkce3ySzWTmDAz20hgQparKs0Fqw2JiRhikOQ9dGkmOUVzr" +
                    "BD3tASwqEfTsVxI0OqQ02pyCo/6S1JA36PFap8er0eeZNHbGAtjhpVbuSi4RDA5QEZVlUKvMO9lCMjQFQH6lkNZgzOobCnvOeE9nbZ6N3913e9X4a9sNuuwbBXN+bbKMcSqAg2e8jQ/XTrFMw5oAYLj" +
                    "l97Fr56ZwoMWPAGI2//JgxjM9kTRh3PVVN1fXFuDvor+74/uu3WBL3Duq3dA732/76qWXJgKY+XLbIYNuntzuqgMfFwBQT82L7hGNc/4vfgNa0OLHCw/VvwAAAABJRU5ErkJggg==";
            }
        } 
        public static string SettingsImageBase64
        {
            // return image in form base64: "data:image/png;base64,"
            get
            {
                return "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAMAAAAp4XiDAAAAnFBMVEUAAABVVVVSUlVUVFW3t7dVVVVTU1WWlpaurq4eGEqJiYlkY2QeGUp8fHwgGkpRUVZqampQT1UhG0siHUsgGkoeGEojH" +
                    "ktmZmYeGEoeGUofGUoeGEozME4eGEogGkonI0seGEofGkoeGUofGUpsbG0jHksnI0wlIUs4Nk4eGEkhG0oiHUsfGUolIUsfGUoxLk0nI0siHUoqJkweGErF5X+XAAAAM3RSTlMADgYDAwsQCAX3B" +
                    "A5GCg4UDBesjYbsNgjh5aZQGtO8IvPDlG8SfnpMKwrJsrBtdjFaoYV6N3ztAAAEOUlEQVRIx4WWCXMaMQyFLRkblr24jwCBHJCkJE1b/f//1ietNwtMJ90h5I1iRbb8+XmdPYMBvnq93n/kQKXFXN" +
                    "/1+m44cP1hkn2TPWfRQSv7mmlZ/RK/fF5ClrlvJFd1HvkqOlLZR7JGRuwcj3LIPFeZjycHkbkcJrGJdgNGpc4QeYE5OE+OvCOV95Ke+xTtBuhwzXKR8OXHXmV0C5FTgecoskhRijaAde2ogqIhMMI8" +
                    "UklcSEYRcitSIEqQ1AwgbRt+ymVwKE38NM/eX/GXF5HKRw4VUjCV/SRbPfnAkD5H2/Asn/W7DneCZ73fTtdIcWHskLKebvdrjd/5OnyNHZaetOhvkfMpk3mmy/YcfPQPkNlcsp8TkUcOjGb6fjM3T8" +
                    "SoMSG3QcJ8tggUEI1hMZsjaeN4onVCIAXBGQ+RnpBRY4nF/ekNy1VpbXo7PrzoAOQ8IWopigZ5v5IzOZSO3ke0DRN1ZBJRlaizCi4ssS8tD5mc7P96m1C1mU1ns03lbcOCRn9KZqQMDRil5B1T1gl" +
                    "C8uddJvZkd5+6hUSsi5woO+XQgFFKXteSFdHQmOro3W630t/TrQHzkqH9pHLQ049Rsp+jtZBjdHb19Kpy+4GsH1uVP2S+vwGGeJvJMUIi4xEkqCQKj5oDeZRsewsMTWX+5kln9cvQiGwb/Atzg3yby" +
                    "/QCmPLZLYpiLTP0ZoEarl4qGmPDSOt8Qs5k/VIsvoCp7HwAdAZnK9RQNMhHVPEUVnIHuWjOT52Awc6iqw8hhCqTD9R3hoaPKulDMuxPrryBkAYYPshpXFUePGxEXsl1wKjci2zQUl9V9VEObMDUIo" +
                    "Uno2QmOw+plHTArGSWZCFSGTD5QQqlBBIpznWUNHIn0yQLmecGzHOGFCZKKSaNEpXRUpIs5GAp/YiJJQNBCjN1tgIZkTKjRqaJDXT5f2osH16Slt+ZDeTF8k9YvgGjTT5ok5msyeyubAXUV5D3TZMT" +
                    "MLHdSk/YSk8GjGUFtq2ETFs5Tg7zT2DCt8B4SlgyecXSkwJjUa9Ygh3DEtHRhcN4hZ8gFX6QpTLBP4Y0+C3a+3IYO2LcHrGPV+6OGLdH7MZh7CA3ttIe5J0d5LGxkw7ylcOYXSSHWVzYRWJH7eIMOe" +
                    "ocRk2po4RaU+rYUVOylM5hVvIe7UrqgEmUJHbOssoRsiupM9ixV4M9vtnlY9Frg71ymGTjsbXxfGnA3Ng4IZqA4aBUYQve28viwSsw3WVxFvm9JL65ksY3V9Ky/u+V5HHxrbqLT4FBim7V/qwXH3HQ" +
                    "Ki0wTOYlITTs4PBFgrTrNbGjfQ43wCRbSZf4nwIPuri4NBt/BQybrTTA0OWrQooyovgETsCMvJa2V4/GVurJQQ/qYVJZNBC3A8oyvfYAguGoNB5MjvS157nmFC016hGF1CJYDIq1b1TfSXyQYFUu3tC" +
                    "+l86k+wuD+H1hP4ORfgAAAABJRU5ErkJggg==";
            }
        } 
        public static string SearchArchivLogoBase64
        {
            // return image in form base64: "data:image/png;base64,"
            get
            {
                return "iVBORw0KGgoAAAANSUhEUgAAAEgAAABICAMAAABiM0N1AAABj1BMVEUAAAAeGEoeGEoeGEoeGEoeGEoeGEoeGEoeGEoeGEoeGEoeGEoeGEoeGEoeGEoeGEoeGEoeGEoeGEoeGEoeGEoeG" +
                    "EoeGEoeGEoeGEoeGEoeGEoeGEr//PweGEr///8fGUr//v4AADYhG0v+/PwAAEAcFkkOA0QbFEnS0NMAAELm5eYZEkgUDEcAADwAAAAXD0gAADD6+fkOA0YEAEP8+vv39vbf3+DDwcW1tLns6eteX" +
                    "G/V09axr7Slo6p1c4EAADgAACn7+vrw7e6zsbatq7GdmqM1NE8oJ0r5+Pj09PTHxck6OlDq6Onh3+HMyc3Ix8txb31ST2dFRFXo5uiJh5GEgow8OFcjHUwAADPx8PDd2919e4ZpZ3dbWGwAABHb2d" +
                    "zZ19qop6yhnqWZl6CRjpZ6eIRJRmBDQlxKSFcqJU/t6+29ur+5t723trqWlZ2NjJRtbHtBPlouKlExLk319fXQzdBjYXJNS2E4NFU5OFQLAUMAAALj4uQSC0RhYmJaWWFDQ1UZFkUhHkAAAB5kY3C" +
                    "JeWt4AAAAHHRSTlMACwb49cGfjHgfA/7t5dG9tbClioBzVkE7JiTMtQ24yQAABnVJREFUWMO9mGd70lAUx7WUtmrVqlVrzbk3CSEhQNijyJ5d0EL33nsP914f3BgKaZMbivo8/l/C5RfOujnn3LhGt1" +
                    "tuKmq5feMf9OzJ4/a2B/fN5vsP2tofP3n2d5Rb7Xc7TXcAXwjumDrvtt/6Q8jt3o57ACA433PO/sLwcKHfyb13WgHgXkfvHxj5tM2EsZ/hdhNrh6O2uVBo2Tbaly87OMaHwdT2tFmbulqB9X2oxKOzA" +
                    "xaKphTRlGUgY8tvL7kxtHY1Y2FLN4DgLpysUIimNKKRxfb63CkAdLdcx+kxA05NrS0jRBGF6Oi6dQaDuacxp6MVMHMcCVANlLbPS7J9HY3MegTg28pe/BmL15u2EFF0/NQH8MjYvIfAJiftvzkI2cOx" +
                    "fD4fC9tJRqJQQmLhoRHnObCedYty0J74yoLD4QD2a2Kc5C7vJMPCc7JdD8HKVc0ayKV4h9g/NjbWLzp4JpYmeT3OATwkWfcIWO5QifheecaXXMxGx4tFW24x6Z/ZzFAErXIsPCLES7arT+EMlHl3YSg" +
                    "o+9RCIxQMDzv5zTSJFJet08WupxWSecUutOFxLyp+iZy+XbB5UaninplAJOsmJWjt0TjIDL6Jqp+HXM6piPK74o7V5ZpfRqUtyTNC9HjCB+arbuoGvGVXvpx9ISyVqsURmC2+Ope2bWjO7f5JzNHQKY" +
                    "buK3UKwBxUn2k749fUNESDm9zCoHfD2W+jiKGTMFyu4C6cOqpyLDn+LHr5aLB8nqGGpsVVYpLT8zPQpXKetjq2I7VE4xf3rpwNDlqovR1nYoAiyW4VWp/W78M2cMcvfPCywpe9+vNfrG8HyRW87oS22" +
                    "p3Za2ILJaqqwbe+TYIvPoNABlHRc2zqreUiFudRDXRuAMIGIPq1u56V98Blr4GCC/xJUHc68EXYekkGIdsSvleLvbhA13PsmH8zrjv98fTTpJciy7Ltu8iAdmBiat5m+d0R3eF9lsvSBiCUZ6BdAd0F" +
                    "MaSeWpniJ7QPD867C3OUgWgbB3eV93KnMHXJkd4FwbOPrj7zgBHnKUNlHNbO32/zJyb+OH3pVysu8NvQZc4oIyRDyBA0UHaansigx3f8ucvpj7Ie/O2V+klg1Dqd6jPmUJb8+zuP5bRux/4wfSXW+aQ" +
                    "g5T9SSBZNhY4YkOJUI/Vx0H77Rksb1lZ2IL6ExeFEbOjVUK5c8AuuVW8jDj3qxG0tN24+wMMhbURfvcVOn8QwjOTEsB1FVEPZ+vGDmzdu3sdjugxEs/uTlRmXyzMNeKyovOjSmaARb7mA78sgM96JkG" +
                    "KRsc9F1xjMvptTCuHkxcmhASk0jM0qiCSUWTo7OwsrF7lgdfzYzBiDFNOKlJFGZH20UKXv0wDAMlOjpD81p5imOlvfDdVE5xygSPi0igycrYQ/SuIEbX01xRYvQDDt2XhJDr+akBp5JySmLh/UxEplX" +
                    "VNxqCSkXCJcjHSru4As/7v9ASXna7KsySVSLdqNtD5gc0YgYJO7k/tRu72eKIlq0T7rdLwJUjrNfheMSNgtpZZcY/WTu0Lns4uLrUTrazrmx9BIL2q+jioXm3LVJg8QIbUXhEYcaw2E4rWr9hZwCULY" +
                    "Vk6hKRBV8cOt+usooieFp5sCoZUP+F79BckRGqmRXdwU6MSHO9RX9ju7/k5flNgmQMsF1tSrNhGOXECXSZGydD0osOaWmwi1rXFXMvqcXE1dD4pMVdsatdGKIx3oNXctCB2noEvT+g0hLWjdcx0IZRk" +
                    "MtzTNaGWP0qjo/EQOnIP3s7uvf1f2FoZubXvMH1l0OZnw6wvNyn2Y2oiFR6NyGU0q7bG2YZ/JaY2jiy5Wg2ElMbEc9F7YnpQbdv0IMZ3SX3CL4hW/8N/erI7X5i4662GhgzjU8EMaEj3iceOqQExK4s" +
                    "TQy/qNRh8qQw15zOLDGusCYXHJI+uH68OL9dHZtPog1MdZlTGLPPilchqPo+BILJvNhkvBAI2utCCyXc+NR9HpmaM9pEFVdfUz+0RSHUXJwzFfUTPTSOhgSzMck8f1eKbhuB6IHDGacZ28QIBUJWdH" +
                    "NJmCUCm+nQIw9zSx0sBux7uJCK1nyR/Z5wtuB4bulmaXLJwrcVAKpim6DrGkB0OxBZfIEpcsxmsf4JLiG7mmoqFiJDIesoVzx1MiIwJW1z7NL6IcPOfvHx7b2Rkb7vf7eUG/iGp+NYbrUldj/2VZ" +
                    "9+/rw18eMzNaW+qChwAAAABJRU5ErkJggg==";
            }
        }
        public static string AdwersLogoBase64
        {
            // return image in form base64: "data:image/png;base64,"
            get
            {
                return "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAAAAAByaaZbAAABZElEQVQYGe3BL2/qUADG4feb7Btcg8GiMDN8gPoJDGJyggQ1M1WDqZmdQJOQ1FRhUDUVFZiaCpImTXOS373bOeVPaWn97vPo" +
                    "P2da1LYaZEct0iA4ptxpCB9nrGEOOBqowFlrkHdqsQbJqFUawsM5GgINsMF5PVFqgBTrpABW6jXC+ZQMkXqFWNVM2lOol8HKniQffPVY4XxJGkOuHiGOvsWYhR4rsQ765sFGD33gvOlHRq6HMqxi" +
                    "qh8hLPWAV2HFst4g1QM+zlxOQeWpW4qVa2w9fUGgTlMcU9YqyNQpotVcHf7kWIUuNhCrw4vB2uhiAsVU7XY4upbASu0MVqxrS0jVyscJdG1SwExtEpyZbiSwV4tJgZXqlg/5SPfecRZqMDDXvSPW" +
                    "SU0hxLozx9mraQG8qCnBidTkGTg+64q3O5TUzH63DXS23YY5/5g0+lBtRUOi2oiLSLXndXBrqbN1cPaq3+ovpGD63MyEYUwAAAAASUVORK5CYII=";
            }
        }
        public static BitmapImage MemoringStreamBitmap(byte[] base64String)
        {
            // Return BitmapImage, after Memoring StreamSourcer
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(base64String))
            {
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            };
            return bitmapImage;
        }

    }
}
