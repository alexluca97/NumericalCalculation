function delta(xk) {
    return (g(xk) / s(xk));
}

function s(xk) {
    return (g(xk + g(xk)) - g(xk)) / g(xk);
}

function g(x) {
    return G2(f, x, 0.000005);
}

function f(x) {
    return x * x - 4 * x + 3;
}

function tema(x0) {
    console.log(x0);

    const eps = 0.00000000001;
    const kmax = 1000000;

    let x = x0;
    k = 0;
    let deltax = 0;

    do {
        if (Math.abs(g(x)) <= eps)
            return x; // x is aprox a solution

        deltax = delta(x);
        x = x - deltax;
        console.log('** ' + x);
        k++;
    }
    while (Math.abs(deltax) >= eps && k <= kmax && Math.abs(deltax) <= 10 ** 8);

    if (Math.abs(deltax) < eps)
        console.log(`${deltax} e aprox = x*`);
    else
        console.log('Divergenta, de incercat schimbarea x0');
}

function G1(F, x, h) {
    return (3 * F(x) - 4 * F(x - h) + F(x - 2 * h)) / 2 * h;
}

function G2(F, x, h) {
    return (-F(x + 2 * h) + 8 * F(x + h) - 8 * F(x - h) + F(x - 2 * h)) / 12 * h;
}

function F2(F, x, h) {
    return (-F(x + 2 * h) + 16 * F(x + h) - 30 * F(x) + 16 * (F(x - h))) / 12 * (h * h);
}

// tema(Math.random() * 10);
const X0 = 2;
let res = tema(X0);
console.log(res);

if (res) {
    if (!G1(f, X0, 0.000005) || G2(f, X0, 0.000005)) {
        console.log('are prima dev 0');

        if (F2(f, X0, 0.000005))
            console.log('E PUNCT MINIM');
        else
            console.log('NU E punct minim');

    } else
        console.log('nu are dev 0');
}