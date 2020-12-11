export const mapGetters = (store, namespace, getters) =>
  Object.fromEntries(
    getters.map((getter) => [
      getter,
      store.getters[`${namespace}/${getter}`],
    ])
  );

export const mapActions = (store, namespace, actions) =>
  Object.fromEntries(
    actions.map((action) => [
      action,
      store._actions[`${namespace}/${action}`][0],
    ])
  );
