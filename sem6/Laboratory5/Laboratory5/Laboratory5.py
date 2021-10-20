import numpy as np
from numpy.linalg import eig
from scipy.linalg import hilbert

def pow(matrix, epsilon, x_0):
    x_1 = matrix @ x_0
    iterations = 1
    lambda_0 = np.dot((x_1), x_0) / np.dot(x_0, x_0)
    while True:
        x_0 = x_1
        x_1 = matrix @ x_1
        lambda_1 = np.dot((x_1), x_0) / np.dot(x_0, x_0)
        if abs(lambda_1-lambda_0) < epsilon or iterations > 5000:
            break
        lambda_0 = lambda_1
        iterations += 1
    return abs(lambda_1), iterations

def scalar_product(matrix, epsilon, x_0):
    iterations = 1
    x_1 = matrix @ x_0
    y_0 = np.copy(x_0)
    y_1 = matrix.T @ y_0
    lambda_0 = np.dot(x_1, y_1) / np.dot(x_0, y_1)
    while True:
        x_0 = x_1
        y_0 = y_1
        x_1 = matrix @ x_1
        y_1 = matrix.T @ y_1
        lambda_1 = np.dot(x_1, y_1) / np.dot(x_0, y_1)
        if abs(lambda_1-lambda_0) < epsilon or iterations > 5000:
            break
        lambda_0 = lambda_1
        iterations += 1
    return abs(lambda_1), iterations

def print_report(matrix):
    for i in range(2, 6):
        print("Текущие epsilon:", 10 ** (-i))
        print("Погрешность решения, найденного с помощью степенного метода:", abs(max(abs(eig(matrix)[0])) - pow(matrix, 10 ** (-i), np.ones(matrix.shape[1]))[0]))
        print("Количество итераций:", pow(matrix, 10 ** (-i), np.ones(matrix.shape[1]))[1])
        print('\n')
        print("Текущие epsilon:", 10 ** (-i))
        print("Погрешность решения, найденного с помощью скалярного метода:", abs(max(abs(eig(matrix)[0])) - scalar_product(matrix, 10 ** (-i), np.ones(matrix.shape[1]))[0]))
        print("Количество итераций:", scalar_product(matrix, 10 ** (-i), np.ones(matrix.shape[1]))[1])
        print('\n')

def print_matrix(matrix):
    for i in range(matrix.shape[0]):
        for j in range(matrix.shape[1]):
            print(matrix[i][j], end = ' ')
        print('\n')


def main():
    print("Матрица Гильберта 3 порядка:")
    matrix_1 = hilbert(3)
    print_matrix(matrix_1)
    print_report(matrix_1)
    print('\n')
    print("Матрица Гильберта 4 порядка:")
    matrix_2 = hilbert(4)
    print_matrix(matrix_2)
    print_report(matrix_2)
    print('\n')
    print("Матрица Гильберта 5 порядка:")
    matrix_3 = hilbert(5)
    print_matrix(matrix_3)
    print_report(matrix_3)
    print('\n')
    print("Матрица Гильберта 6 порядка:")
    matrix_4 = hilbert(5)
    print_matrix(matrix_4)
    print_report(matrix_4)
    print('\n')
    print("Матрица из учебника Фадеева:")
    matrix_5 = np.array([[4.2,-3.4,0.3], [4.7,-3.9,0.3], [-5.6,5.2,0.1]])
    print_matrix(matrix_5)
    print_report(matrix_5)

main()