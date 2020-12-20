<template>
  <div class="home" :key="randomSeed">
    <ArtImage :imgTitle="title" :imgSrc="imgSrc" />
    <button @click="randomize()">New Art</button>
    <Likes :imgId="randomSeed" />
  </div>
</template>

<script>
// @ is an alias to /src
import ArtImage from "@/components/ArtImage.vue";
import Likes from "@/components/Likes.vue";
import { useRoute, useRouter } from "vue-router";
import { ref } from "vue";

export default {
  name: "Home",
  components: {
    ArtImage,
    Likes
  },
  setup() {
    const route = useRoute();
    const router = useRouter();

    const randomSeed = ref(route.params.artId);
    const imgSrc = ref(null);
    const title = ref("Generating Title ...");

    if (!randomSeed.value) {
      setRandomSeed();
    }

    function randomIntFromInterval(min, max, pad) {
      const randomNumber = Math.floor(Math.random() * (max - min + 1) + min);
      return pad ? `${randomNumber}`.padStart(pad, "0") : randomNumber;
    }

    function setRandomSeed() {
      randomSeed.value = `${randomIntFromInterval(0, 450000, 10)}`;
      router.push({ path: `/image/${randomSeed.value}` });
    }

    async function reload() {
      imgSrc.value = `/art/${randomSeed.value}`;
      title.value = "Generating Title ...";
      await fetch(`/title/${randomSeed.value}`)
        .then(result => result.json())
        .then(result => (title.value = result.title));
    }

    function randomize() {
      setRandomSeed();
      reload();
    }

    reload();

    return {
      randomSeed,
      imgSrc,
      title,
      randomize
    };
  }
};
</script>
