import scipy
import scipy.linalg
import copy
import time


# ex1
def read_file(path):
    """
    :param path: calea fisierului de unde se citesc date
    :return: n - dimensiunea sistemului, A - matricea rara, b - vectorul termenilor liberi
    """
    A = dict()
    b = list()

    with open(path, "r") as f:
        n = int(f.readline())
        data = f.readline()

        for i in range(0, n):
            l = list()
            A[i] = l

        data = f.readline()
        while data is not "\n":
            values = data.split("\n")[0].split(", ")
            nr = values[0]
            i = values[1]
            j = values[2]

            flag = False
            for k in A[int(i)]:
                if k[1] == int(j):
                    flag = True
                    k[0] = k[0] + float(nr)

            if flag is False:
                A[int(i)].append([float(nr), int(j)])

            data = f.readline()

        data = f.readline()
        while data:
            b.append(float(data))
            data = f.readline()

    return n, A, b


def data_validation(A):
    """
    :param A: matrice rara
    :return: True daca matricea are diagonala principala nenula, False altfel
    """
    for i in A.keys():
        flag = False
        for j in A[i]:
            if j[1] == i:
                flag = True
        if flag is False:
            return False
    return True


# ex2
def mat_item(A, i, j):
    """
    :param A: matricea A
    :param i: linia i
    :param j: coloana j
    :return: elementul de pe linia i si coloana j din matricea A
    """
    for k in A[i]:
        if k[1] == j:
            return k[0]
    return 0


def sor(n, A, b, omega):
    """
    :param n: dimensiunea sistemului
    :param A: matricea rara
    :param b: vectorul termenilor liberi
    :param omega: parametrul omega
    :return: solutia sistemului Ax = b prin metoda relaxarii succesive sau False daca A nu este valida
    """
    if data_validation(A) is False:
        return False

    eps = pow(10, -8)
    xc = [0 for i in range(0, n+1)]
    xp = [0 for i in range(0, n+1)]
    kmax = 10000

    for i in range(0, n):
        nr1 = sum([j[0] + xc[j[1]] for j in A[i] if j[1] < i])

        nr2 = sum([j[0] + xp[j[1]] for j in A[i] if j[1] >= i])

        xc[i] = xp[i] + omega / mat_item(A, i, i) * (b[i] - nr1 - nr2)

    k = 1
    delta_x = scipy.linalg.norm([xc[i] - xp[i] for i in range(len(xc))])

    while k <= kmax and eps <= delta_x <= pow(10, 8):
        xp = copy.copy(xc)

        for i in range(0, n):
            nr1 = sum([j[0] * xc[j[1]] for j in A[i] if j[1] < i])

            nr2 = sum([j[0] * xp[j[1]] for j in A[i] if j[1] >= i])

            xc[i] = xp[i] + omega / mat_item(A, i, i) * (b[i] - nr1 - nr2)

        delta_x = scipy.linalg.norm([xc[i] - xp[i] for i in range(0, len(xc))])

        k += 1

    if delta_x < eps:
        return xc
    else:
        return "divergenta"


def afis(indice, omega):
    in_path = "m_rar_2019_" + str(indice) + ".txt"
    out_path = "m" + str(indice) + "omega" + str(omega).replace(".", "") + ".txt"
    n, A, b = read_file(in_path)
    x = sor(n, A, b, 0.8)
    open(out_path, "w").write(str(x).replace(", ", ",\n"))

    print(out_path + " - done")


t = time.time()
afis(1, 0.8)
afis(1, 1.0)
afis(1, 1.2)

afis(2, 0.8)
afis(2, 1.0)
afis(2, 1.2)

afis(3, 0.8)
afis(3, 1.0)
afis(3, 1.2)

afis(4, 0.8)
afis(4, 1.0)
afis(4, 1.2)

afis(5, 0.8)
afis(5, 1.0)
afis(5, 1.2)

print(time.time() - t)