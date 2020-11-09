<template>
  <div class="home" :key="randomSeed">
    <ArtImage :imgTitle="title" :imgSrc="imgSrc" />
    <button @click="randomize()"> New Art</button>
  </div>
</template>

<script>
// @ is an alias to /src
import ArtImage from "@/components/ArtImage.vue";
import { ref } from "vue";

export default {
  name: "Home",
  components: {
    ArtImage
  },
  setup() {
    function randomIntFromInterval(min, max, pad) {
      const randomNumber = Math.floor(Math.random() * (max - min + 1) + min);
      return pad ? `${randomNumber}`.padStart(pad, "0") : randomNumber;
    }
    async function randomize() {
      randomSeed.value = `${randomIntFromInterval(0, 450000, 10)}`;
      imgSrc.value = `${window.artexplorerfrontendConfig.ART_CREATOR_API}/${randomSeed.value}`;
      title.value = 'Generating Title ...';
      await fetch(`${window.artexplorerfrontendConfig.ART_TITLE_API}/${randomSeed.value}`)
              .then(result => result.json())
              .then(result => {
                title.value = result.title
              });      
    }
    const randomSeed = ref(null);
    const imgSrc = ref(null);
    const title = ref('Generating Title ...');
    randomize();
    return {
      randomSeed,
      imgSrc,
      title,
      randomize
    };
  }
};
</script>
