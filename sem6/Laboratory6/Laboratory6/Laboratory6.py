import numpy as np
import math
from copy import copy
from numpy.linalg import norm
from scipy.linalg import hilbert, eig

def max_not_diagonal(matrix):
    curr_i = 0
    curr_j = 1
    max = matrix[curr_i][curr_j]
    for i in range(matrix.shape[0]):
        for j in range(i + 1, matrix.shape[0]):
            if abs(max) < abs(matrix[i][j]):
                max = matrix[i][j]
                curr_i = i
                curr_j = j
    return curr_i, curr_j


def jacobi_method(matrix,epsilon,strategy): #метод Якоби 
        iterations = 0
        i = 0
        j = 0
        while True:
            t = np.identity(matrix.shape[0])
            if strategy == "max":
                i,j = max_not_diagonal(matrix)
            else:
                if (j < (matrix.shape[0]-1) and j+1!=i):
                    j+=1
                elif j == matrix.shape[0]-1:
                    i+=1
                    j = 0
                else:
                    j+=2
            if i==matrix.shape[0]-1 and j==matrix.shape[0]:
                    return np.diag(matrix), iterations
            if abs(matrix[i, j]) < epsilon:
                return np.diag(matrix), iterations
            iterations += 1
            phi = 0.5*(math.atan((2*matrix[i, j])/(matrix[i,i]-matrix[j,j])))
            c,s = math.cos(phi), math.sin(phi)
            t[i,i] = c
            t[j,j] = c
            t[i,j] = -s
            t[j,i] = s
            matrix = t.T @matrix @t

def is_in_gersh_circle(matrix, eigenvalue):
    circles = []
    is_in_circle = True
    for i in range(matrix.shape[0]):
        circles.append((matrix[i,i],sum(abs(matrix[i]))-abs(matrix[i,i])))
    for el in circles:
        if abs(eigenvalue - el[0]) < el[1]:
            is_in_circle = True
    print(is_in_circle)

def print_matrix(matrix):
    for i in range(matrix.shape[0]):
        for j in range(matrix.shape[1]):
            print(matrix[i][j], end = ' ')
        print('\n')

def print_report(matrix):
    for i in range(2, 6):
        print("Текущий epsilon:", 10 ** (-i))
        print("Погрешность в найденном вектор с.ч. c помощью стратегии максимального недиагонального элемента:", norm(np.sort(jacobi_method(matrix, 10 ** (-i), "max")[0]) - np.sort(eig(matrix)[0])))
        print("Число итераций:", jacobi_method(matrix, 10 ** (-i), "max")[1])
        print('\n')
        print("Погрешность в найденном вектор с.ч. c помощью стратегии цикла:", norm(np.sort(jacobi_method(matrix, 10 ** (-i), "circle")[0]) - np.sort(eig(matrix)[0])))
        print("Число итераций:", jacobi_method(matrix, 10 ** (-i), "circle")[1])
        print('\n')
    print("Проверим, попали ли с.ч., найденные с помощью стратегии максимального недиагонального элемента:")
    for el in jacobi_method(matrix, 10 ** (-5), "max")[0]:
        is_in_gersh_circle(matrix, el)
    print('\n')
    print("Проверим, попали ли с.ч., найденные с помощью стратегии цикла:")
    for el in jacobi_method(matrix, 10 ** (-5), "max")[0]:
        is_in_gersh_circle(matrix, el)
    

def main():
    print("Матрица из учебника Фадеевой:")
    matrix_1 = np.array([[-5.509882,1.870086,0.422908],
              [0.287865,-11.811654,5.7119],
              [0.049099,4.308033,-12.970687]])
    print(matrix_1)
    print_report(matrix_1)
    print('\n')
    print("Матрица Гильберта 3-го порядка:")
    matrix_2 = hilbert(3)
    print(matrix_2)
    print_report(matrix_2)
    print('\n')
    print("Матрица Гильберта 4-го порядка:")
    matrix_3 = hilbert(4)
    print(matrix_3)
    print_report(matrix_3)
    print('\n')
    print("Матрица Гильберта 10-го порядка:")
    matrix_4 = hilbert(10)
    print(matrix_4)
    print_report(matrix_4)

main()