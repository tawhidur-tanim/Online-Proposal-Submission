<template>
  <div class="mask">
    <div id="cover-spin">
      <div class="swapping-squares-spinner" style="margin:auto">
        <div class="square"></div>
        <div class="square"></div>
        <div class="square"></div>
        <div class="square"></div>
      </div>
    </div>
  </div>
</template>


<style scoped>

  .mask {
    position: fixed;
    z-index: 99999;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, .5);
    display: table;
    transition: opacity .3s ease;
  }
  #cover-spin {
    z-index: 999999;
    position: fixed;
    top: 50%;
    left: 50%;
    margin-top: -50px;
    margin-left: -100px;
  }

  .swapping-squares-spinner, .swapping-squares-spinner * {
    box-sizing: border-box;
  }
  .swapping-squares-spinner {
    height: 65px;
    width: 65px;
    position: relative;
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
  }

    .swapping-squares-spinner .square {
      height: calc(65px * 0.25 / 1.3);
      width: calc(65px * 0.25 / 1.3);
      animation-duration: 1000ms;
      border: calc(65px * 0.04 / 1.3) solid #ff1d5e;
      margin-right: auto;
      margin-left: auto;
      position: absolute;
      animation-iteration-count: infinite;
    }

      .swapping-squares-spinner .square:nth-child(1) {
        animation-name: swapping-squares-animation-child-1;
        animation-delay: 500ms;
      }

      .swapping-squares-spinner .square:nth-child(2) {
        animation-name: swapping-squares-animation-child-2;
        animation-delay: 0ms;
      }

      .swapping-squares-spinner .square:nth-child(3) {
        animation-name: swapping-squares-animation-child-3;
        animation-delay: 500ms;
      }

      .swapping-squares-spinner .square:nth-child(4) {
        animation-name: swapping-squares-animation-child-4;
        animation-delay: 0ms;
      }

  @keyframes swapping-squares-animation-child-1 {
    50% {
      transform: translate(150%,150%) scale(2,2);
    }
  }

  @keyframes swapping-squares-animation-child-2 {
    50% {
      transform: translate(-150%,150%) scale(2,2);
    }
  }

  @keyframes swapping-squares-animation-child-3 {
    50% {
      transform: translate(-150%,-150%) scale(2,2);
    }
  }

  @keyframes swapping-squares-animation-child-4 {
    50% {
      transform: translate(150%,-150%) scale(2,2);
    }
  }
</style>
