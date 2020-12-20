<template>
  <li>
    <a :href="'/' + image.imageId">
      <img :src="'/art/' + image.imageId" :alt="image.imageId" />
    </a>
    <div style="position: relative">
      <button @click="deleteImage(image)">&times;</button>
    </div>
  </li>
</template>

<script>
import { useStore } from 'vuex';
import {mapGetters} from '../store';

export default {
  name: "GalleryImage",
  props: {
    image: Object
  },
  setup(props, { emit }) {
    const store = useStore();
    const { oidcIdToken, oidcUser } = mapGetters(store, "oidcStore", ["oidcIdToken", "oidcUser"]);

    async function deleteImage(image) {
      const headers = { Authorization: `Bearer ${oidcIdToken}`, "Content-Type": "application/json" };
      const body = {
        ...image,
        email: oidcUser.email,
      };
      await fetch(`https://localhost:8081/likes/`, { 
        method: "DELETE",
        headers,
        body: JSON.stringify(body),
      });
      emit("delete-image", image);
    }

    return { deleteImage };
  }
};
</script>

<style lang="scss" scoped>
li {
  list-style-type: none;
  width: min-content;
}

a:hover + div > button, button:hover {
  display: block;
}

button {
  border-radius: 50%;
  font-weight: bolder;
  background-color: red;
  color: white;
  height: 24px;

  position: absolute;
  right: 0;
  top: -262px;
  display:none;
}

img {
  width: 256px;
  height: 256px;
  transition: 0.1s;
  &:hover {
    transition: 0.2s;
    transform: scale(1.1);
  }
}
</style>
