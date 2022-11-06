// Pomocnicze
const N_MIN = 5;
const N_MAX = 20;
const N_DEFAULT = 10;
const NUMBER_MIN = 1;
const NUMBER_MAX = 99;
const ADDITIONAL_CANVAS_COUNT = 20;

const errorMessage = document.getElementById("error-message");
const table = document.getElementById("table")
const canvasWrapper = document.getElementById("canvas-wrapper");

function randomBetweenInclusive(minInc, maxInc) {
  return Math.floor(Math.random() * (maxInc - minInc + 1)) + minInc;
}

function clearCanvas(ctx) {
  ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height);
}

function rescaleCanvas(canvas) {
  canvas.width = canvas.clientWidth;
  canvas.height = canvas.clientHeight;
  return canvas;
}

function drawLine(ctx, fx, fy, tx, ty) {
  ctx.beginPath();
  ctx.moveTo(fx, fy);
  ctx.lineTo(tx, ty);
  ctx.strokeStyle = "#3b82f6";
  ctx.lineWidth = 2;
  ctx.stroke();
}

// Zadanie 1
const nStr = window.prompt("Podaj liczbę n wierszy i kolumn tabeli:") ?? "";
let n = parseInt(nStr, 10);
if (isNaN(n) || n < N_MIN || n > N_MAX) {
  n = N_DEFAULT;
  errorMessage.innerText = `Podana liczba (${nStr}) jest błędna, więc przyjęto n = ${n}.`;
}

const numbers = Array.from({ length: n }).map(() =>
  randomBetweenInclusive(NUMBER_MIN, NUMBER_MAX)
);

const tbody = document.createElement("tbody");

for (let trow = -1; trow < n; trow++) {
  const tr = document.createElement("tr");

  for (let tcol = -1; tcol < n; tcol++) {
    const isHeader = trow === -1 || tcol === -1;
    const cell = document.createElement(isHeader ? "th" : "td");

    if (isHeader) {
      cell.innerText = (numbers[trow === -1 ? tcol : trow] ?? "").toString();
    } else {
      const product = numbers[trow] * numbers[tcol];
      cell.innerText = product.toString();
      const remainder = product % 2;

      if (remainder === 0) {
        cell.classList.add("even")
      } else {
        cell.classList.add("odd")
      }
    }

    tr.appendChild(cell);
  }

  tbody.appendChild(tr);
}

table.appendChild(tbody);

// Zadanie 2
const fragment = document.createDocumentFragment();
for (let ci = 0; ci < ADDITIONAL_CANVAS_COUNT; ci++) {
  const canvas = document.createElement("canvas");
  canvas.width = randomBetweenInclusive(10, 250);
  canvas.height = randomBetweenInclusive(10, 250);
  fragment.appendChild(canvas);
}
canvasWrapper.appendChild(fragment);

document.querySelectorAll("canvas").forEach((canvas) => {
  const ctx = canvas.getContext("2d");

  canvas.addEventListener("mousemove", (event) => {
    const { width, height } = rescaleCanvas(canvas);
    const { offsetX: x, offsetY: y } = event;

    clearCanvas(ctx);
    drawLine(ctx, 0, y, width, y);
    drawLine(ctx, x, 0, x, height);
  });

  canvas.addEventListener("mouseleave", () => clearCanvas(ctx));
});
