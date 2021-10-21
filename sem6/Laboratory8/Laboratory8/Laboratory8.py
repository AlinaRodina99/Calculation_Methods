import numpy as np
import matplotlib.pyplot as plt
from scipy.special import eval_jacobi
from scipy.misc import derivative
from scipy.integrate import quad
from numpy.linalg import solve, det
from math import exp, sin, log
from copy import copy


def p_n_k(n,k):
    return lambda t: (1-t**2)*eval_jacobi(n,k,k,t)

def p_n_k_first_der(n,k): 
    return lambda t: derivative(p_n_k(n,k),t)

def p_n_k_second_der(n,k): 
    return lambda t: derivative(p_n_k_first_der(n,k),t)

def galerkin(a, b,functions,N):
    k,p,q,f = functions
    phi = [p_n_k(i,1) for i in range(N)]
    dphi = [p_n_k_first_der(i,1) for i in range(N)]
    ddphi = [p_n_k_second_der(i,1) for i in range(N)]
    A = np.array([lambda x: k(x)*ddphi[i](x)+p(x)*dphi[i](x)+q(x)*phi[i](x) for i in range(N)])
    C = np.array([quad(lambda t: f(t)*phi[i](t),a,b)[0] for i in range(N)])
    B = np.zeros([N,N])
    for i in range(N):
        for j in range(N):
            B[i,j] = quad(lambda t: phi[i](t)*A[j](t),a,b)[0]
    alpha = solve(B,C)
    return lambda t: sum([alpha[i]*phi[i](t) for i in range(N)])

def main():
    print("-(4-x/5-2x)u'' + ((1-x)/2)u' + 1/2 ln(3+x) u = 1 +x/3, u(-1)=u(1)=0")
    functions_0 = []
    functions_0.append(lambda x: -((4-x)/(5-2*x)))
    functions_0.append(lambda x: (1-x)/2)
    functions_0.append(lambda x: 1/2 * log(3 + x))
    functions_0.append(lambda x: 1 + x/3)
    fig, axes = plt.subplots(3, 2, figsize=(20, 15))
    fig, axes = plt.subplots(3, 2, figsize=(20, 15))
    for i in range(3):
        for j in range(2):
            if j == 0:
                N, h = 3, 0.05
            else:
                N, h = 11, 0.01
            u = galerkin(-1,1 ,functions_0,N)
            a = -1
            b = 1
            n = round((b - a) / h)
            x1 = np.zeros(n + 1)
            y = np.zeros(n + 1)
            for t in range(n + 1):
                x1[t] = a + t* h
                y[t] = u(x1[t])
            axes[i,j].plot(x1, y, marker='.', color='red', mec='black', ms=10)
            axes[i,j].set_title("N = {}".format(N-1))

    print("(x-2/x+2)u'' + xu' + (1-sin(x)) u = x^2, u(-1)=u(1)=0")
    functions_1 = []
    functions_1.append(lambda x: (x-2)/(x+2))
    functions_1.append(lambda x: x)
    functions_1.append(lambda x: 1 - sin(x))
    functions_1.append(lambda x: x ** 2)
    fig, axes = plt.subplots(3, 2, figsize=(20, 15))
    fig, axes = plt.subplots(3, 2, figsize=(20, 15))
    for i in range(3):
        for j in range(2):
            if j == 0:
                N, h = 3, 0.05
            else:
                N, h = 11, 0.01
            u = galerkin(-1,1 ,functions_1,N)
            a = -1
            b = 1
            n = round((b - a) / h)
            x1 = np.zeros(n + 1)
            y = np.zeros(n + 1)
            for t in range(n + 1):
                x1[t] = a + t* h
                y[t] = u(x1[t])
            axes[i,j].plot(x1, y, marker='.', color='red', mec='black', ms=10)
            axes[i,j].set_title("N = {}".format(N-1))

    print("-(7-x/8+3x)u'' + (1 + x/3)u' + (1-1/2 * e^(x/2)) u = 1/2 - x/3, u(-1)=u(1)=0")
    functions_2 = []
    functions_2.append(lambda x: -((7-x)/(8+3*x)))
    functions_2.append(lambda x: 1 + x/3)
    functions_2.append(lambda x: 1 - 0.5 * exp(x/2))
    functions_2.append(lambda x: 0.5 - x/3)
    fig, axes = plt.subplots(3, 2, figsize=(20, 15))
    fig, axes = plt.subplots(3, 2, figsize=(20, 15))
    for i in range(3):
        for j in range(2):
            if j == 0:
                N, h = 3, 0.05
            else:
                N, h = 11, 0.01
            u = galerkin(-1,1 ,functions_2,N)
            a = -1
            b = 1
            n = round((b - a) / h)
            x1 = np.zeros(n + 1)
            y = np.zeros(n + 1)
            for t in range(n + 1):
                x1[t] = a + t* h
                y[t] = u(x1[t])
            axes[i,j].plot(x1, y, marker='.', color='red', mec='black', ms=10)
            axes[i,j].set_title("N = {}".format(N-1))



main()
