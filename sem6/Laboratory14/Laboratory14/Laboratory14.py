import numpy as np
import matplotlib.pyplot as plt
from numpy.linalg import norm
from inspect import getfullargspec

def compute_gradient(u,x0,dx=1e-8): 
    n = len(x0)
    gradient = np.zeros(n)
    for i in range(n):
        delta = np.array([0 if j != i else dx for j in range(n)])
        gradient[i] = (u(*(x0+delta))-u(*x0))/dx
    return gradient

def gradient_descent(f,learning_rate,eps,dx=1e-8): 
    max_iters = 2000
    num_of_iters = 0
    n = len(getfullargspec(f)[0])
    teta0 = np.array([np.random.normal() for _ in range(n)])
    teta1 = teta0 - learning_rate*compute_gradient(f,teta0,dx)
    while(norm(teta0-teta1)>eps and num_of_iters < max_iters):
        num_of_iters +=1
        teta0 = teta1
        teta1 = teta0 - learning_rate*compute_gradient(f,teta0,dx)
    return teta0, num_of_iters

def nesterov_method(f,eps):
    n = len(getfullargspec(f)[0])
    y1 = np.array([np.random.normal() for _ in range(n)])
    z = np.array([np.random.normal() for _ in range(n)])
    k = 0
    a1 = 1
    x0 = y1
    a0 = norm(y1-z)/norm(compute_gradient(f,y1)-compute_gradient(f,z))
    i = 0
    while f(*y1)-f(*(y1-2**(-i)*a0*compute_gradient(f,y1)))<2**(-i-1)*a0*(norm(compute_gradient(f,y1))**2):
        i+=1
    a1 = 2**(-i)*a0
    x1 = y1-a1*compute_gradient(f,y1)
    a2 = (1+(4*a1**2+1)**0.5)/2
    y2 = x1+((a1-1)*(x1-x0))/a2
    while (norm(y1-y2)>eps and k < 2000):
        k+=1
        a0,a1,x0,y1 = a1,a2,x1,y2
        i = 0
        while f(*y1)-f(*(y1-2**(-i)*a0*compute_gradient(f,y1)))<2**(-i-1)*a0*(norm(compute_gradient(f,y1))**2):
            i+=1
        a1 = 2**(-i)*a0
        x1 = y1-a1*compute_gradient(f,y1)
        a2 = (1+(4*a1**2+1)**0.5)/2
        y2 = x1+((a1-1)*(x1-x0))/a2
    return y2,k

def g(x):
    return 4*x+11

N = 100
a,b = -10,10
h = (b-a)/N
X = np.array([a+i*h for i in range(N)])
Y = np.array([g(x) for x in X])


def loss_function(X,Y):
    return lambda a,b: np.sum(np.array([0.5*(a*x+b-y)**2 for x,y in zip (X,Y)]))

f = loss_function(X,Y)

def main():
    print("Алгоритм градиентного спуска")
    for i in range(2,7,2):
        for j in range(2,7,2):
            lmbda,eps = 10**(-i),10**(-j)
            res, k = gradient_descent(f,lmbda,eps)
            row = [lmbda,eps,k,abs(res[0]-4),abs(res[1]-11),f(*res)]
            print("Лямбда:", lmbda)
            print("Epsilon:", eps)
            print("Количество итераций:", k)
            print("Погрешность в найденном коэффициенте a:", abs(res[0]-4))
            print("Погрешность в найденном коэффициенте b:", abs(res[1]-11))
            print("Значение функции потерь:", f(*res))
            print('\n')

    print("Алгоритм Нестерова")
    for j in range(2,9,2):
        eps = 10**(-j)
        res, k = nesterov_method(f,eps)
        row = [eps,k,abs(res[0]-4),abs(res[1]-11),f(*res)]
        print("Epsilon:",eps)
        print("Количество итераций:",k)
        print("Погрешность в найденном коэффициенте a:", abs(res[0]-4))
        print("Погрешность в найденном коэффициенте b:", abs(res[1]-11))
        print("Значение функции потерь:", f(*res))
        print('\n')

main()
