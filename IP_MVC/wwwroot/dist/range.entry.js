/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
var __webpack_exports__ = {};
/*!**********************************************!*\
  !*** ./src/ts/Views/Flow/Questions/range.ts ***!
  \**********************************************/

window.addEventListener('DOMContentLoaded', () => {
    const selectedValue = document.getElementById('selectedValue');
    selectedValue.value = selectedValue.dataset.earlierAnswer || '';
});
document.querySelector('input[type="range"]').addEventListener('input', (e) => {
    const selectedValue = document.getElementById('selectedValue');
    selectedValue.value = e.target.value;
});

/******/ })()
;
//# sourceMappingURL=range.entry.js.map