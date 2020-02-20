#!/usr/bin/python

# -*- coding: utf-8 -*-



import numpy as np

from scipy import sparse

from datetime import datetime



inFilename = [

    "a_example.txt",

    "b_lovely_landscapes.txt",

    "c_memorable_moments.txt",

    "d_pet_pictures.txt",

    "e_shiny_selfies.txt"]

outFilename = [

    "a_output.txt",

    "b_output.txt",

    "c_output.txt",

    "d_output.txt",

    "e_output.txt"]



################################################################################

N = 0 # número de fotos

H = [] # las fotos horizontales

V = [] # las verticales

FINAL = [] # el resultado final

TAGS = {} # tag_string => tag_int

TAGN = 0

MATRIX = []

IDS = []

LENS = []



################################################################################

def main():

    for i in range(1, 2):

        print("START")

        readInput(inFilename[i])

        print("RUNNING...")

        algorithm()

        print("FINISH")

        writeOutput(outFilename[i])





def readInput(filename):

    global N, H, V, TAGS, TAGN, MATRIX, IDS, LENS



    with open(filename) as f:

        lines = f.readlines()

    lines = [x.strip() for x in lines]



    # line 1

    N = int(lines[0].split()[0])

    lines.pop(0)

    LENS= np.zeros((N,1), dtype=np.int)



    # line 2..n

    indexesX=[]

    indexesY=[]

    for i, line in enumerate(lines):

        fields= line.split()

        LENS[i][0]= int(fields[1])

        tags= fields[2:]

        # en TAGS se indexa por tag y devuelve el código

        for tag in tags:

            if tag not in TAGS:

                TAGS[tag]=TAGN

                TAGN+=1

            indexesX.append(i)

            indexesY.append(TAGS[tag])

    data= np.ones(len(indexesX), dtype=np.int8)

    MATRIX= sparse.coo_matrix((data, (indexesX, indexesY))).tocsr()

    IDS= np.arange(N)



def writeOutput(filename):

    file = open(filename,"w") 

  

    file.write(str(len(FINAL)))

    file.write('\n')

    for item in FINAL:

        file.write(str(item))

        file.write('\n')

    file.close()





################################################################################

def algorithm():

    global N, TAGN, H, V, FINAL, MATRIX, IDS, LENS



    totalscore= 0

    i= 0

    FINAL.append(0)

    while np.size(MATRIX, axis=0) > i:

        print("-"*20)



        row_i= MATRIX[i].todense()

        len_i= LENS[i]

        IDS= np.delete(IDS, i)

        LENS= np.delete(LENS, i, axis= 0)



#        s= datetime.now()

        MATRIX= sparse.vstack([MATRIX[:i,:], MATRIX[i+1:,:]])

#        print(datetime.now()-s)



#        s= datetime.now()

        equals= MATRIX.multiply(row_i)

#        print(datetime.now()-s)



#        s= datetime.now()

        sum_equals= equals.sum(axis=1)

#        print(datetime.now()-s)



        #sum_diffsA= -sum_equals + np.count_nonzero(row_i)

        sum_diffsA= -sum_equals + len_i



        #s= datetime.now()

        #row_inv= (~(row_i.astype(np.bool))).astype(np.int8)

        #diffsB= MATRIX.multiply(row_inv)

        #print(datetime.now()-s)



        #sum_diffsB= diffsB.sum(axis=1)

        sum_diffsB= -sum_equals + LENS



        sums= np.column_stack((sum_equals, sum_diffsA, sum_diffsB))

        scores= np.amin(sums, axis=1)

        next_i= np.argmax(scores)

        score= np.max(scores)

        totalscore+= score

        i= next_i

        FINAL.append(IDS[i])

        print(str(np.size(MATRIX, axis=0))+": "+str(IDS[i])+" "+str(score)+" points")

    print("Total score: "+str(totalscore))





################################################################################

# esto siempre al final

if __name__ == '__main__':

    main()

