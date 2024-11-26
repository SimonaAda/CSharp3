# Feedback Final Project Backed

Backend je funkční, povedlo se implementovat požadované změny.

Hlavní výtka - nějak nám neprochází všechny testy (jeden konkrétně) - Get_ReadByIdWhenSomeItemAvailable_ReturnsOk

Důvodem proč testy máme je abychom zkontrolovali že jsme změnami nic nerozbili. Tento padající test je sice vinou chyby v testu, ovšem good practise je ideálně si před pushem do repozitáře zkontrolovat testy že je všechno ok - což se zde očividně nestalo.

Proč bys měla sama mít zájem pouštět testy?

To bych rozdělil do dvou rovin
A) slušnost - v repozitáři na gitu by měl být ideálně funkční kód, tedy když provedeš změny je fajn si spustit testy a checknout že je všechno ok
B) trest - template je nastavený tak že by se měly každý večer spustit build + testy a mělo by ti to spamovat mail že je tam problém (nevím jestli se to děje, ale takový byl původní záměr )
    Teď sranda stranou, představ si že vyvíjíš ne ve větvi main, ale v nějaké své vývojové větvi. A až dokončíš svůj vývoj, tak chceš mergnout (v githubu je to pull request, v gitlabu merge request) změny do hlavní větve. Pokud programuješ v nějaké firmě, tak mergování téměř určitě bude provádět CI pipeline, která spustí build, testy a pokud narazí na problém, tak ti to zablokuje. A ty to pak musíš vyřešit, což ti bere čas navíc.
