<template>
  <div>
    <h1>Favourites</h1>
    <p v-if="loading">Loading...</p>
    <ul v-else-if="images.length">
      <GalleryImage
        v-for="image in images"
        :key="image.imageId"
        :image="image"
        v-on:delete-image="deleteImage($event)"
      />
    </ul>
    <p v-else>Looks like there's nothing here.</p>
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
    const { oidcUser, oidcIdToken } = mapGetters(store, "oidcStore", ["oidcUser", "oidcIdToken"]);
    const images = ref([]);
    const loading = ref(true);

    async function fetchImages() {
      const headers = {Authorization: `Bearer ${oidcIdToken}`};
      const response = await fetch(
        `/api/likes/liked?email=${oidcUser.email}`,
        {headers}
      );
      images.value = await response.json();
      loading.value = false;
    }

    function deleteImage(image) {
      const { imageId, imageVersion } = image;
      images.value = images.value.filter(i => !(i.imageId === imageId && i.imageVersion === imageVersion));
    }

    fetchImages();

    return { loading, images, deleteImage };
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
