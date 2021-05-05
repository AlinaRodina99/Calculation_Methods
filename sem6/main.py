import numpy as np
from numpy.linalg import inv, cond


def calculate_cond(a):
    return cond(a)


def find_solution(a, b):
    return inv(a).dot(b)


def find_approximate_solution(a, b, epsilon):
    solution = find_solution(a, b)
    a += epsilon
    b += epsilon
    approximate_solution = inv(a).dot(b)
    return abs(approximate_solution - solution)


# Тесты для матриц из методички Пакулиной
A = np.matrix([[-400.6, 199.8], [1198.8, -600.4]])
B = np.matrix([[50.0], [40.0]])
print(calculate_cond(A))
print(find_solution(A, B))
print(find_approximate_solution(A, B, 10e-2))


A = np.matrix([[-400.6, 199.8], [1198.8, -600.4]])
B = np.matrix([[50.0], [40.0]])
print(calculate_cond(A))
print(find_solution(A, B))
print(find_approximate_solution(A, B, 10e-5))


A = np.matrix([[-400.6, 199.8], [1198.8, -600.4]])
B = np.matrix([[50.0], [40.0]])
print(calculate_cond(A))
print(find_solution(A, B))
print(find_approximate_solution(A, B, 10e-8))


A = np.matrix([[-401.52, 200.16], [1200.96, -601.68]])
B = np.matrix([[30.0], [20.0]])
print(calculate_cond(A))
print(find_solution(A, B))
print(find_approximate_solution(A, B, 10e-2))


A = np.matrix([[-401.52, 200.16], [1200.96, -601.68]])
B = np.matrix([[30.0], [20.0]])
print(calculate_cond(A))
print(find_solution(A, B))
print(find_approximate_solution(A, B, 10e-5))


A = np.matrix([[-401.52, 200.16], [1200.96, -601.68]])
B = np.matrix([[30.0], [20.0]])
print(calculate_cond(A))
print(find_solution(A, B))
print(find_approximate_solution(A, B, 10e-8))


# Тесты для матриц Гильберта
A = np.matrix([[1, 1/2], [1/2, 1/3]])
B = np.matrix([[6.0], [-1.0]])
print(calculate_cond(A))
print(find_solution(A, B))
print(find_approximate_solution(A, B, 10e-2))


A = np.matrix([[1, 1/2], [1/2, 1/3]])
B = np.matrix([[6.0], [-1.0]])
print(calculate_cond(A))
print(find_solution(A, B))
print(find_approximate_solution(A, B, 10e-5))


A = np.matrix([[1, 1/2], [1/2, 1/3]])
B = np.matrix([[6.0], [-1.0]])
print(calculate_cond(A))
print(find_solution(A, B))
print(find_approximate_solution(A, B, 10e-8))


A = np.matrix([[1, 1/2, 1/3], [1/2, 1/3, 1/4], [1/3, 1/4, 1/5]])
B = np.matrix([[6.0], [-1.0], [2.0]])
print(calculate_cond(A))
print(find_solution(A, B))
print(find_approximate_solution(A, B, 10e-2))


A = np.matrix([[1, 1/2, 1/3], [1/2, 1/3, 1/4], [1/3, 1/4, 1/5]])
B = np.matrix([[6.0], [-1.0], [2.0]])
print(calculate_cond(A))
print(find_solution(A, B))
print(find_approximate_solution(A, B, 10e-5))


A = np.matrix([[1, 1/2, 1/3], [1/2, 1/3, 1/4], [1/3, 1/4, 1/5]])
B = np.matrix([[6.0], [-1.0], [2.0]])
print(calculate_cond(A))
print(find_solution(A, B))
print(find_approximate_solution(A, B, 10e-8))


# Тест для трехдиагональной матрицы с диагональным преобладанием
A = np.matrix([[7.0, 1.0, 0.0, 0.0], [1.0, 8.0, 1.0, 0.0], [0.0, 1.0, 9.0, 1.0], [0.0, 0.0, 1.0, 10.0]])
B = np.matrix([[6.0], [-1.0], [2.0], [-5.0]])
print(calculate_cond(A))
print(find_solution(A, B))
print(find_approximate_solution(A, B, 10e-2))


A = np.matrix([[7.0, 1.0, 0.0, 0.0], [1.0, 8.0, 1.0, 0.0], [0.0, 1.0, 9.0, 1.0], [0.0, 0.0, 1.0, 10.0]])
B = np.matrix([[6.0], [-1.0], [2.0], [-5.0]])
print(calculate_cond(A))
print(find_solution(A, B))
print(find_approximate_solution(A, B, 10e-5))


A = np.matrix([[7.0, 1.0, 0.0, 0.0], [1.0, 8.0, 1.0, 0.0], [0.0, 1.0, 9.0, 1.0], [0.0, 0.0, 1.0, 10.0]])
B = np.matrix([[6.0], [-1.0], [2.0], [-5.0]])
print(calculate_cond(A))
print(find_solution(A, B))
print(find_approximate_solution(A, B, 10e-8))
