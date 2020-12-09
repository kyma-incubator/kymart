<template>
  <div class="artImage">
    <h1>{{ imgTitle }}</h1>
    <img :src="imgSrc" :alt="imgTitle" />
  </div>
</template>

<script>
export default {
  name: "ArtImage",
  props: {
    imgTitle: String,
    imgSrc: String
  }
};
</script>

<style scoped>
img {
  max-width: 100%;
}
</style>
