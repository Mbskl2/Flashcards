using System.Collections.Generic;
using FlashCards.DataAccess;
using FlashCards.DataAccess.Models;

namespace Flashcards.DA.InMemory
{
    public class InMemoryFlashcardService : IFlashcardService
    {
        public IList<IFlashcard> Get(int i)
        {
            return new List<IFlashcard>
            {
                new Flashcard("biegać", "laufen",
                    new List<IUseCase>
                    {
                        new UseCase("Lubię biegać rano wzdłuż rzeki.", "Ich laufe morgens gerne am Fluss entlang."),
                        new UseCase("Wyszedłem z psem przed pracą.", "Ich lief mit meinem Hund vor der Arbeit."),
                        new UseCase("Przebiegłem wczoraj 5 km.", "Ich bin gestern fünf Kilometer gelaufen.")
                    }),
                new Flashcard("prosić", "bitten",
                    new List<IUseCase>
                    {
                        new UseCase("Hans poprosił telefonicznie w komisariacie o pomoc.", "Hans bittet im Kommissariat telefonisch um Hilfe."),
                        new UseCase("Poprosiłem koleżankę o radę.", "Ich bat meine Freundin um Rat."),
                        new UseCase("On zaprosił mnie do siebie.", "Er hat mich zu sich gebeten")
                    }),
                new Flashcard("łapać", "fingen",
                    new List<IUseCase>
                    {
                        new UseCase("Maria łapie motyle w siatkę.", "Maria fängt mit einem Netz Schmetterlinge."),
                        new UseCase("Nie złapałem ani jednej ryby.", "Ich fing keinen einzigen Fisch."),
                        new UseCase("Złapaliśmy 2 lisy.", "Wir haben zwei Füchse gefangen.")
                    }),
                new Flashcard("dwa", "zwei",
                    new List<IUseCase>
                    {
                        new UseCase("Mam 2 psy.", "Ich habe zwei Hunde.")
                    }),
                new Flashcard("członek", "der Mitglied", new List<IUseCase>())
            };
        }
    }
}