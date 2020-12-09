<template>
  <div>
    <h1>Favourites</h1>
    <ul>
      <GalleryImage
        v-for="image in images"
        :key="image.id"
        :image="image"
        v-on:delete-image="deleteImage($event)"
      />
    </ul>
  </div>
</template>

<script>
// @ is an alias to /src
import { useStore } from "vuex";
import { ref } from "vue";
import { mapGetters } from "../store";
import GalleryImage from "../components/GalleryImage.vue";

export default {
  name: "Favourites",
  components: {
    GalleryImage
  },
  setup() {
    const store = useStore();
    const { oidcUser } = mapGetters(store, "oidcStore", ["oidcUser"]);
    const images = ref([]);

    async function fetchImages() {
      const response = await fetch(
        `http://0.0.0.0:8081/likes?email=${oidcUser.email}`
      );
      images.value = await response.json();
    }

    function deleteImage(imageId) {
      images.value = images.value.filter(image => image.id !== imageId);
    }

    fetchImages();

    return { images, deleteImage };
  }
};
</script>

<style scoped>
ul {
  display: grid;
  grid-gap: 16px;
  place-content: center;
  grid-template-columns: repeat(auto-fit, 262px);
}
</style>
