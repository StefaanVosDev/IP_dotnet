/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
var __webpack_exports__ = {};
/*!**********************************************!*\
  !*** ./src/ts/Views/Flow/Questions/range.ts ***!
  \**********************************************/

var _a;
window.addEventListener('DOMContentLoaded', () => {
    const selectedValue = document.getElementById('selectedValue');
    selectedValue.value = selectedValue.dataset.earlierAnswer || '';
});
(_a = document.querySelector('input[type="range"]')) === null || _a === void 0 ? void 0 : _a.addEventListener('input', (e) => {
    const selectedValue = document.getElementById('selectedValue');
    selectedValue.value = e.target.value;
});

/******/ })()
;
//# sourceMappingURL=range.entry.js.map