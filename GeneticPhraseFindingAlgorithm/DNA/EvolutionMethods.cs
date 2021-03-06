﻿using System;

namespace GeneticAlgorithmTest
{
    public class EvolutionMethods
    {
        private static readonly Random rng = new Random();

        public static Chromosome[] CrossOverElites(Chromosome[] elites, int amount)
        {
            Chromosome[] children = new Chromosome[amount];

            for (int i = 0; i < amount; i++)
            {
                children[i] = CrossOver(elites[rng.Next(0, elites.Length)], elites[rng.Next(0, elites.Length)]);
            }

            return children;
        }

        private static Chromosome CrossOver(Chromosome chromOne, Chromosome chromTwo)
        {
            string chromOneString = chromOne.GetGenes();
            string chromTwoString = chromTwo.GetGenes();

            string returnChromosomeString = "";
            for (int i = 0; i < chromOneString.Length; i++)
            {
                if (rng.Next(0, 2) == 1) returnChromosomeString += chromOneString[i];
                else returnChromosomeString += chromTwoString[i];
            }

            return new Chromosome(returnChromosomeString);
        }

        public static Chromosome[] MutateDna(decimal mutationRate, Chromosome[] chromosomes)
        {
            string newGene = "";
            bool needToMutate = false;
            foreach (Chromosome chromosome in chromosomes)
            {
                for (int i = 0; i < chromosome.GetGenes().Length; i++)
                {
                    var x = rng.Next(0, 100);
                    if (x >= mutationRate * 100) newGene += chromosome.GetGenes()[i];
                    else
                    {
                        needToMutate = true;
                        newGene += Helpers.ReturnRandomChar();
                    }
                }
                if (needToMutate)
                {
                    chromosome.Mutate(newGene);
                    needToMutate = false;
                }
                newGene = "";
            }
            return chromosomes;
        }

        public static Chromosome[] MutateDnaAdvanced(decimal mutationRate, int currentGeneration, Chromosome[] chromosomes)
        {
            string newGene = "";
            bool needToMutate = false;

            for (var chromosomeIndex = (int)Math.Floor(Helpers.GetPercentageOfAlphas(currentGeneration) * chromosomes.Length); 
                chromosomeIndex < chromosomes.Length; chromosomeIndex++)
            {
                Chromosome chromosome = chromosomes[chromosomeIndex];

                for (int geneIndex = 0; geneIndex < chromosome.GetGenes().Length; geneIndex++)
                {
                    if (rng.Next(0, 100) > mutationRate * 100) newGene += chromosome.GetGenes()[geneIndex];
                    else
                    {
                        needToMutate = true;
                        newGene += Helpers.ReturnGeneticallyCloseChar(chromosome.GetGenes()[geneIndex]);
                    }
                }
                if (needToMutate)
                {
                    chromosome.Mutate(newGene);
                    needToMutate = false;
                }
                newGene = "";
            }
            return chromosomes;
        }
    }
}