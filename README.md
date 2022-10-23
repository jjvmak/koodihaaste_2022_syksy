# koodihaaste_2022_syksy
Haasteeseen on vastattu .NET 6.0 konsoliaplikaatiolla, joka hyödyntää [Spectre.Console](https://spectreconsole.net/) kirjastoa. Ratkaisu poikkeaa ulkonäöllisesti ja toimintaperiaatteeltaan tehtävänannossa mainitusta (fullstack) web-sovelluksesta. Tähän ratkaisumalliin on päädytty, koska haluna oli toteuttaa jotain oman (perus)osaamisen ulkopuolelta.

### Ratkaisuperiaate
Pelin perusmekaniikat on toteuttu [tehtävänannon](https://koodihaaste.solidabis.com/tehtava) kuvaamalla tavalla. Peliin on lisätty kuitenkin hieman satunaisuutta, jotta taisteluihin saadaan yllätyksellisyyttä.

- Hyökkääjä voi tehdä kriittistä vahinkoa:
    - Hyökkäysvuorollaan pelaaja "rollaa d20 noppaa". Jos silmäluku on > 18, hyökkääjä tekee kriittistä vahinkoa vastustajaan.
    - Vahinkobonus: `hyökkäys x 1.2`
- Peli sisältää kolme satunnaisesti arvottua spesiaali esinettä:
    - `Taikamiekka`: hyökkäysbonus (x1.2)
    - `Mithrilkilpi`: puolustusbonus (x.1.4)
    - `Nopeuskengät`: hyökkäysviivebonus (x0.8) lisäksi mahdollisuus väistää vihollisen hyökkäys

Peli ei noudata kokemaamme aikaa, vaan pelisilmukka simuloi tapahtumia tikeissä. Tästä johtuen pelimaailmassa aika kuluu nopeammin kuin reaalimaailmassa. Kyseinen toteutus on tehty katsojien viihtyvyyttä silmälläpitäen, sillä jotkin taistelijat ovat varsinaisia Titaaneja, joiden väliset taistelu voi venyä varsin pitkiksi.

Repositoryn juuresta löytyy `release` hakemisto, johon on käännetty ja julkaistu valmiiksi ajatteva sovellus (win-x64 ja linux-x64)

## Vaatimukset
- Internetyhteys
    - Ohjelma käyttää [Finellin](https://fineli.fi/fineli/fi/avoin-data) rajapintaa 
- [.NET 6.0](https://dotnet.microsoft.com/en-us/download) 

## Ajaminen
Seuraavat komennot tulee ajaa repositoryn juuressa.
```
dotnet build
```
```
dotnet run --project koodihaaste_2022_syksy
```
https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build
https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run

Testien ajaminen.
```
dotnet test
```
## Ajettavan tiedoston julkaisu
`release`hakemistosta löytyy valmiiksi käännetyt ja ajettavat tiedostot Windowsille ja Linuxille.

Seuraavilla kommenoilla voidaan kääntää ajettavat tiedostot: 

`win-x64`
```
dotnet publish --output <haluttupolku> --runtime win-x64 --configuration Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true
```
`linux-x64`
```
dotnet publish --output <haluttupolku> --runtime linux-x64 --configuration Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true
```
- Huom. Linux-ympäristössä voidaan joutua asettamaan erikseen suoritusoikeudet, jotta ohjelma voidaan suorittaa. (Testattu vain[ Windows Subsystem for Linuxilla](https://learn.microsoft.com/en-us/windows/wsl/install))
- ```chmod 777 ./<appname>```


https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-publish