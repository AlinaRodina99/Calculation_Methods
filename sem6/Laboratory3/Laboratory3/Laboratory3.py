import pandas as pd
import numpy as np
from numpy.linalg import norm, cond, solve, inv
from scipy.linalg import hilbert
import math

def iterational_method(alpha,beta,x0,eps): #общая схема итерационного метода
    num_of_iters = 1
    x1 = alpha @ x0 + beta
    while (norm(x1-x0)>eps and num_of_iters < 500):
        x0 = x1
        x1 = alpha @ x0 + beta
        num_of_iters += 1
    return x1, num_of_iters

def simple_iterational_method(a,b,x0,eps): #метод простых итераций
    alpha = np.zeros([a.shape[0],a.shape[1]])
    beta = np.zeros(b.shape[0])
    for i in range(alpha.shape[0]):
        for j in range(alpha.shape[1]):
            if i != j:
                alpha[i,j] = -a[i,j]/a[i,i]
                beta[i] = b[i]/a[i,i]
    return iterational_method(alpha,beta,x0,eps)

def seidel_method(a,b,x0,eps): #метод Зейделя
    n, m = a.shape[0], a.shape[1]
    l,r,d = [np.zeros([n,m]) for _ in range(3)]
    for i in range(n):
        for j in range(m):
            if i > j:
                l[i,j] = a[i,j]
            elif i < j:
                r[i,j] = a[i,j]
            else:
                d[i,j] = a[i,j]
    beta = inv(d+l)
    return iterational_method(-beta@r,beta@x0,x0,eps)

def print_matrix(matrix):
    for i in range(matrix.shape[0]):
        for j in range(matrix.shape[1]):
            print(matrix[i][j], end = ' ')
        print('\n')

def print_report_simple_iterations(matrix, b):
    print("Метод простых итераций")
    for i in range(3, 12, 4):
        print("Текущий epsilon: ", 10 ** (-i))
        print("Погрешность решения: ", norm(solve(matrix, b) - simple_iterational_method(matrix, b, b, 10**(-i))[0]))
        print("Количество итераций: ", simple_iterational_method(matrix, b, b, 10**(-i))[1])
    print('\n')

def print_report_seidel(matrix, b):
    print("Метод Зейделя")
    for i in range(3, 12, 4):
        print("Текущий epsilon: ", 10 ** (-i))
        print("Погрешность решения: ", norm(solve(matrix, b) - seidel_method(matrix, b, b, 10**(-i))[0]))
        print("Количество итераций: ", seidel_method(matrix, b, b, 10**(-i))[1])
    print('\n')

def print_report_relax_method(matrix, b):
    print("Релаксационный метод")
    for i in range(3, 12, 4):
        print("Текущий epsilon: ", 10 ** (-i))
        print(solve(matrix,b))
        print("Погрешность решения: ", norm(solve(matrix, b) - relax_method(matrix, b, b, 10**(-i))[0]))
        print("Количество итераций: ", relax_method(matrix, b, b, 10**(-i))[1])
    print('\n')

def main():
    print("Матрица из методички Пакулиной:")
    matrix_1 = np.array([[-401.64, 200.12], [21200.72,  -601.76]])
    print_matrix(matrix_1)
    b_1 = np.array([[-202.92], [40596.16]])
    print_report_simple_iterations(matrix_1, b_1)
    print_report_seidel(matrix_1, b_1)
    print("Матрица из методички Пакулиной")
    matrix_2 = np.array([[-400.94, 200.02], [1200.12, -600.96]])
    print_matrix(matrix_2)
    b_2 = np.array([[-201.82], [597.36]])
    print_report_simple_iterations(matrix_2, b_2)
    print_report_seidel(matrix_2, b_2)
    print("Матрица Гильберта 3-го порядка")
    print_report_simple_iterations(hilbert(3), np.array([[11/6], [13/12], [47/60]]))
    print_report_seidel(hilbert(3), np.array([[11/6], [13/12], [47/60]]))
    print("Матрица Гильберта 4-го порядка")
    print_report_simple_iterations(hilbert(4), np.array([[25/12], [77/60], [19/20], [319/420]]))
    print_report_seidel(hilbert(3), np.array([[11/6], [13/12], [47/60]]))
    print("Матрица Гильберта 5-го порядка")
    print_report_simple_iterations(hilbert(5), np.array([[137/60], [29/20],[153/140],[743/840],[1879/2520]]))
    print_report_seidel(hilbert(3), np.array([[11/6], [13/12], [47/60]]))
    print("Матрица Гильберта 6-го порядка")
    print_report_simple_iterations(hilbert(6), np.array([[49/20], [223/140], [341/280], [2509/2520], [2131/2520], [20417/27720]]))
    print_report_seidel(hilbert(3), np.array([[11/6], [13/12], [47/60]]))
    
main()